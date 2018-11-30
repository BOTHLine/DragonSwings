using UnityEngine;

public class HookResponder : MonoBehaviour
{
    // Components
    public Rigidbody2D _Rigidbody2D;
    public PushBox _PushBox;
    public HurtBox _HurtBox;

    // References
    public bool _AutoAim;
    public int _AutoAimPriority;

    // Variables
    public Weight _Weight;

    private Transform _OldParent;

    private System.Collections.IEnumerator _CurrentThrowRoutine;

    private RigidbodyType2D _OldRigidbodyType2D;

    // Events
    public UnityEngine.Events.UnityEvent OnHitByHookUnityEvent;
    public UnityEngine.Events.UnityEvent OnObjectLandedUnityEvent;

    // Mono Behaviour
    private void Awake()
    {
        _Rigidbody2D = GetComponentInParent<Rigidbody2D>();
        _PushBox = transform.GetComponentInSiblings<PushBox>();
        _HurtBox = transform.GetComponentInSiblings<HurtBox>();

        _OldParent = transform.parent.parent;
    }

    // Methods
    public void HitByHook()
    {
        OnHitByHookUnityEvent.Invoke();
    }

    public void AttachToObject(Transform targetObject)
    {
        transform.parent.parent = targetObject.transform;
        _PushBox._Collider2D.enabled = false;
        if (_HurtBox != null) { _HurtBox._Collider2D.enabled = false; }
        _OldRigidbodyType2D = _Rigidbody2D.bodyType;
        _Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        transform.parent.transform.position = targetObject.transform.position;
    }

    public void DetachFromObject()
    {
        transform.parent.parent = _OldParent;
        _PushBox._Collider2D.enabled = true;
        if (_HurtBox != null) { _HurtBox._Collider2D.enabled = true; }
        _Rigidbody2D.bodyType = _OldRigidbodyType2D;
    }

    public void StartThrow(Vector2 targetPosition)
    {
        _CurrentThrowRoutine = Throw(targetPosition);
        StartCoroutine(_CurrentThrowRoutine);
    }

    private System.Collections.IEnumerator Throw(Vector2 targetPosition)
    {
        float flyCounter = 0.0f;
        float flyTime = 20.0f; // TODO Remove magic number flyTime;
        float height = 3.0f; // TODO Remove magic number height;

        Vector2 startPosition = transform.position;

        while (targetPosition.SquaredDistanceTo(transform.position) >= 0.0001f)
        {
            Vector2 nextPosition = CalculateParabola(startPosition, targetPosition, height, flyCounter / flyTime);
            transform.parent.position = nextPosition;

            flyCounter++;
            yield return new WaitForEndOfFrame();
        }
        OnObjectLandedUnityEvent.Invoke();
        StopCoroutine(_CurrentThrowRoutine);
    }

    // TODO Move Parabola to Utils class?
    private Vector2 CalculateParabola(Vector2 start, Vector2 end, float height, float time)
    {
        float parabolicT = time * 2 - 1;
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector2 travelDirection = end - start;
            Vector2 result = start + time * travelDirection;
            result.y += (-parabolicT * parabolicT + 1) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector2 travelDirection = end - start;
            Vector2 levelDirecteion = end - new Vector2(start.x, end.y);
            Vector2 up = new Vector2(0.0f, 1.0f);
            //if (end.y > start.y) up = -up;
            Vector2 result = start + time * travelDirection;
            result += ((-parabolicT * parabolicT + 1) * height) * up;
            return result;
        }
    }
}
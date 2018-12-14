using UnityEngine;

public class HookResponder : MonoBehaviour
{
    // Components
    public Rigidbody2D _Rigidbody2D;
    public SpriteRenderer _SpriteRenderer;
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
        _SpriteRenderer = this.GetComponentInSiblings<SpriteRenderer>();
        _PushBox = this.GetComponentInSiblings<PushBox>();
        _HurtBox = this.GetComponentInSiblings<HurtBox>();

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
        _OldRigidbodyType2D = _Rigidbody2D.bodyType;
        _Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        transform.parent.transform.position = targetObject.transform.position;
        _SpriteRenderer.sortingLayerName = "Foreground";
        _PushBox.gameObject.SetActive(false);
    }

    public void DetachFromObject()
    {
        transform.parent.parent = _OldParent;
        _Rigidbody2D.bodyType = _OldRigidbodyType2D;
        _SpriteRenderer.sortingLayerName = "Objects";
        // _PushBox.gameObject.SetActive(true);
    }

    public void StartThrow(Vector2 targetPosition, float flyTime, float flyHeight)
    {
        DetachFromObject();

        _CurrentThrowRoutine = Throw(targetPosition, flyTime, flyHeight);
        StartCoroutine(_CurrentThrowRoutine);
    }

    private System.Collections.IEnumerator Throw(Vector2 targetPosition, float flyTime, float flyHeight)
    {
        float flyCounter = 0.0f;

        Vector2 startPosition = transform.position;

        Vector2 realTargetPosition = targetPosition + (startPosition - targetPosition).normalized * 0.5f;

        while (realTargetPosition.SquaredDistanceTo(transform.position) >= 0.0001f && flyCounter < flyTime)
        {
            Vector2 nextPosition = Utils.CalculatePositionOnParabola(startPosition, realTargetPosition, flyHeight, flyCounter / flyTime);
            transform.parent.position = nextPosition;

            flyCounter++;
            yield return new WaitForEndOfFrame();
        }
        transform.parent.position = Utils.CalculatePositionOnParabola(startPosition, realTargetPosition, flyHeight, 1.0f);
        _PushBox.gameObject.SetActive(true);
        ObjectLanded();
        StopCoroutine(_CurrentThrowRoutine);
    }

    protected virtual void ObjectLanded()
    { OnObjectLandedUnityEvent.Invoke(); }
}
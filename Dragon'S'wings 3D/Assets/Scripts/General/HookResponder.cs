using System.Collections;
using UnityEngine;

public class HookResponder : MonoBehaviour
{
    // Components
    public Rigidbody _Rigidbody;
    public Renderer _Renderer;
    public PushBox _PushBox;
    public HurtBox _HurtBox;

    // References
    public bool _AutoAim;
    public int _AutoAimPriority;

    // Variables
    public Weight _Weight;

    private Transform _OldParent;

    public bool _IsThrowing;

    // private RigidbodyType _OldRigidbodyType2D;

    // Events
    public UnityEngine.Events.UnityEvent OnHitByHookUnityEvent;
    public UnityEngine.Events.UnityEvent OnObjectLandedUnityEvent;

    // Mono Behaviour
    private void Awake()
    {
        _Rigidbody = GetComponentInParent<Rigidbody>();
        _Renderer = this.GetComponentInSiblings<Renderer>();
        _PushBox = this.GetComponentInSiblings<PushBox>();
        _HurtBox = this.GetComponentInSiblings<HurtBox>();

        _OldParent = transform.parent.parent;
    }

    // Methods
    public void HitByHook()
    {
        OnHitByHookUnityEvent.Invoke();
    }

    public void AttachToTransform(Transform targetObject)
    {
        transform.parent.parent = targetObject;
        transform.parent.position = targetObject.position;
        _PushBox.Disable();
        _Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        _Rigidbody.isKinematic = true;
    }

    public void DetachFromTransform()
    {
        transform.parent.parent = _OldParent;
    }

    public void StartThrow(float timePerSegment, Vector3[] segments)
    {

        Debug.Log("Start Throw!");
        _IsThrowing = true;

        _Rigidbody.useGravity = false;
        _Rigidbody.isKinematic = false;
        _Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _PushBox.Enable();
        DetachFromTransform();
        IEnumerator throwRoutine = ThrowRoutine(timePerSegment, segments);
        StartCoroutine(throwRoutine);
    }

    private IEnumerator ThrowRoutine(float timePerSegment, Vector3[] segments)
    {
        float lastTime = Time.time;
        float elapsedTime = 1.0f;
        int index = 0;
        Debug.Log("Time per Segment: " + timePerSegment);
        for (int i = 1; _IsThrowing && i < segments.Length - 1; i++)
        {
            elapsedTime = Time.time - lastTime;
            float elapsedDistance = (segments[i - 1] - segments[i]).magnitude;
            Debug.Log("Elapsed Time:" + elapsedTime);
            Debug.Log("Elapsed Distance: " + elapsedDistance);
            Debug.Log("Velocity: " + elapsedDistance / elapsedTime);
            lastTime = Time.time;
            yield return new WaitForSeconds(timePerSegment);
            _Rigidbody.MovePosition(segments[i]);
            index = i;
        }

        _Rigidbody.velocity = ((segments[index + 1] - segments[index]) / elapsedTime) /* Time.deltaTime*/;

        Debug.Log("Neue Velocity: " + _Rigidbody.velocity.magnitude);

        _Rigidbody.useGravity = true;
    }

    public void StopThrow()
    {
        _IsThrowing = false;
        _Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    /*
    public void StartThrow(Vector3 targetPosition, float flyTime, float flyHeight)
    {
        DetachFromObject();

        _CurrentThrowRoutine = Throw(targetPosition, flyTime, flyHeight);
        StartCoroutine(_CurrentThrowRoutine);
    }

    private System.Collections.IEnumerator Throw(Vector3 targetPosition, float flyTime, float flyHeight)
    {
        float flyCounter = 0.0f;

        Vector3 startPosition = transform.position;

        Vector3 realTargetPosition = targetPosition + (startPosition - targetPosition).normalized * 0.5f;

        while (realTargetPosition.SquaredDistanceTo(transform.position) >= 0.0001f && flyCounter < flyTime)
        {
            Vector3 nextPosition = Utils.CalculatePositionOnParabola(startPosition, realTargetPosition, flyHeight, flyCounter / flyTime);
            transform.parent.position = nextPosition;

            flyCounter++;
            yield return new WaitForEndOfFrame();
        }
        transform.parent.position = Utils.CalculatePositionOnParabola(startPosition, realTargetPosition, flyHeight, 1.0f);
        _PushBox.gameObject.SetActive(true);
        ObjectLanded();
        StopCoroutine(_CurrentThrowRoutine);
    }
    */

    protected virtual void ObjectLanded()
    { OnObjectLandedUnityEvent.Invoke(); }
}
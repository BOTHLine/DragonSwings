using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hook : MonoBehaviour
{
    // Components
    private Rigidbody _Rigidbody;

    // Variables
    private HookAbility _HookAbility;

    private float _HookSpeed;
    private bool _FlyingBack;

    private float _OldMass;
    private float _OldDrag;

    private HookResponder _AttachedHookResponder;

    private bool _HitSomething;

    public GameEvent _HookReturn;

    // Mono Behaviour
    private void Awake()
    {
        _Rigidbody = GetComponentInChildren<Rigidbody>();

        _OldMass = _Rigidbody.mass;
        _OldDrag = _Rigidbody.drag;
    }

    private void FixedUpdate()
    {
        if (_FlyingBack)
        {
            Vector3 targetVector = _HookAbility.transform.position - transform.position;
            _Rigidbody.velocity = targetVector.normalized * (_AttachedHookResponder == null ? _HookSpeed : _HookSpeed / 2.0f);

            if (targetVector.sqrMagnitude <= (_Rigidbody.velocity * Time.fixedDeltaTime * 2.0f).sqrMagnitude)
            { _HookAbility.HookReachedPlayer(); }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_HitSomething) { return; }
        _HitSomething = true;
        _HookAbility.HookHitSomething(collision.collider);
    }

    // Methods
    public void Initialize(HookAbility hookAbility, float hookSpeed)
    {
        _HookAbility = hookAbility;
        _HookSpeed = hookSpeed;
    }

    public void Shoot(Vector3Complex aim)
    {
        _HitSomething = false;

        transform.parent = null;
        transform.position = aim.StartPoint + aim.Direction * 0.05f;
        // transform.LookAt(aim.EndPoint);
        // transform.LookAt2D(aim.EndPoint, -90.0f);

        _Rigidbody.isKinematic = false;
        _Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _Rigidbody.velocity = (aim.Direction * _HookSpeed);
    }

    public void ResetVelocity()
    { _Rigidbody.velocity = _Rigidbody.velocity.normalized * _HookSpeed; }

    public void StopHook()
    { _Rigidbody.velocity = Vector3.zero; }

    public void FlyBack()
    {
        _HookReturn.Raise();
        _FlyingBack = true;
    }

    public void AttachHookResponder(HookResponder hookResponder)
    {
        if (_AttachedHookResponder != null) return;
        _AttachedHookResponder = hookResponder;
        hookResponder.AttachToObject(transform);

        _Rigidbody.mass = hookResponder._Rigidbody.mass;
        _Rigidbody.drag = hookResponder._Rigidbody.drag;
    }

    public HookResponder DetachHookResponder()
    {
        HookResponder hookResponder = _AttachedHookResponder;
        hookResponder?.DetachFromObject();
        _AttachedHookResponder = null;

        _Rigidbody.mass = _OldMass;
        _Rigidbody.drag = _OldDrag;

        return hookResponder;
    }

    public void Reset()
    {
        _FlyingBack = false;
        _Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        _Rigidbody.isKinematic = true;
        _Rigidbody.velocity = Vector3.zero;
        _Rigidbody.angularVelocity = Vector3.zero;

        transform.parent = _HookAbility.transform;
        transform.localPosition = Vector3.zero;
    }
}
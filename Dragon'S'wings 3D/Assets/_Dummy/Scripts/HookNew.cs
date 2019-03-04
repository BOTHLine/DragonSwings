using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HookNew : MonoBehaviour
{
    // Components
    private Rigidbody _Rigidbody;
    private PushBox _PushBox;

    // References
    [SerializeField] private Vector3Reference _PlayerPosition;

    // Settings
    [SerializeField] private FloatReference _HookDamage;
    [SerializeField] private FloatReference _HookSpeed;

    // Variables
    private Vector3Complex _Aim;
    public HookResponder _AttachedHookResponder { get; private set; }

    private bool _IsFlying = true;
    private bool _IsFlyingBack = false;

    private Vector3 _LastPosition;

    // Events
    [SerializeField] private GameEvent _OnPullStartRaise;
    [SerializeField] private GameEvent _OnHookResetRaise;

    // Mono Behaviour
    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        _PushBox = GetComponentInChildren<PushBox>();
    }

    private void FixedUpdate()
    {
        if (_IsFlying)
        {
            Vector3 position = transform.position;
            if (IsWithinTargetPosition(position, _Aim.EndPoint) || HasOvershotTargetPosition(position, _Aim.EndPoint))
            {
                StopFlying();
                _Rigidbody.position = _Aim.EndPoint;
                _OnPullStartRaise.Raise();
            }
        }
        else if (_IsFlyingBack)
        {
            Vector3 position = transform.position;
            if (IsWithinTargetPosition(position, _PlayerPosition.Value) || HasOvershotTargetPosition(position, _PlayerPosition.Value))
            {
                _OnHookResetRaise.Raise();
            }
            else
            {
                _LastPosition = position;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HookResponder responder = collision.gameObject.GetComponent<HookResponder>();
        if (responder == null)
        {
            FlyBack();
        }
        else
        {
            switch (responder._Weight)
            {
                case Weight.None:
                    break;
                case Weight.Light:
                    FlyBack();
                    break;
                case Weight.Medium:
                    FlyBack();
                    _OnPullStartRaise.Raise();
                    break;
                case Weight.Heavy:
                    StopFlying();
                    _OnPullStartRaise.Raise();
                    break;
            }
        }
    }

    // Methods
    public void Shoot(Vector3Complex aim)
    {
        _Aim = aim;
        _Rigidbody.velocity = _Aim.Direction * _HookSpeed.Value;
        _LastPosition = transform.position;
    }

    private void StopFlying()
    {
        _PushBox.Disable();
        _Rigidbody.velocity = Vector3.zero;
        _IsFlying = false;
    }

    private void FlyBack()
    {
        _IsFlying = false;
        _IsFlyingBack = true;
    }

    private void ReturnToPlayer()
    { _Rigidbody.velocity = -_Aim.Direction * _HookSpeed.Value; }

    private void AttachHookResponder(HookResponder responder)
    {
        _AttachedHookResponder = responder;
        responder.AttachToTransform(transform);
    }

    private bool IsWithinTargetPosition(Vector3 position, Vector3 targetPosition)
    { return (position - targetPosition).sqrMagnitude <= 0.0001f; }

    private bool HasOvershotTargetPosition(Vector3 position, Vector3 targetPosition)
    { return (position - _LastPosition).sqrMagnitude >= (targetPosition - _LastPosition).sqrMagnitude; }
}
using UnityEngine;

public class HookAbilityNew : MonoBehaviour
{
    // Components
    private Rigidbody _Rigidbody;
    private HookNew _CurrentHook;

    // References
    [SerializeField] private HookNew _HookPrefab;
    [SerializeField] private Vector3ComplexReference _HookAim;

    [SerializeField] private Transform _AttachedHookResponderParent;

    // Settings
    [SerializeField] private FloatReference _PullSpeed;

    // Variables
    private bool _IsPulling = false;
    private Vector3 _LastPosition;

    private HookResponder _AttachedHookResponder;

    // Events
    [SerializeField] private GameEvent _OnHookShoot;
    [SerializeField] private GameEvent _OnHookReset;
    [SerializeField] private GameEvent _OnPullStart;
    [SerializeField] private GameEvent _OnPullFinish;

    // Mono Behaviour
    private void Awake()
    {
        _Rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_IsPulling)
        {
            Vector3 position = transform.position;
            Vector3 hookPosition = _CurrentHook.transform.position;
            if (IsWithinTargetPosition(position, hookPosition) || HasOvershotTargetPosition(position, hookPosition))
            {
                StopPull();
                _Rigidbody.position = hookPosition;
            }
            else
            {
                _LastPosition = position;
                _Rigidbody.velocity = (hookPosition - position).normalized * _PullSpeed.Value;
            }
        }
    }

    // Methods
    public void TryShoot()
    {
        if (CanShoot())
        { Shoot(); }
    }

    public void ResetHook()
    {
        _AttachedHookResponder = _CurrentHook._AttachedHookResponder;
        if (_AttachedHookResponder != null)
        { _AttachedHookResponder.AttachToTransform(_AttachedHookResponderParent); }

        Destroy(_CurrentHook.gameObject);
        _CurrentHook = null;

        _OnHookReset.Raise();
    }

    private bool CanShoot()
    { return _CurrentHook == null && _HookAim.Value.Magnitude != 0; }

    private void Shoot()
    {
        _CurrentHook = Instantiate(_HookPrefab, transform.position, Quaternion.identity);
        _CurrentHook.Shoot(new Vector3Complex(transform.position, _HookAim.Value.Vector));
        _OnHookShoot.Raise();
    }

    public void StartPull()
    {
        _LastPosition = transform.position;
        _IsPulling = true;

        _Rigidbody.velocity = (_CurrentHook.transform.position - _LastPosition).normalized * _PullSpeed.Value;

        _OnPullStart.Raise();
    }

    public void StopPull()
    {
        _Rigidbody.velocity = Vector3.zero;
        _IsPulling = false;
        ResetHook();
        _OnPullFinish.Raise();
    }

    private bool IsWithinTargetPosition(Vector3 position, Vector3 targetPosition)
    { return (position - targetPosition).sqrMagnitude <= 0.0001f; }

    private bool HasOvershotTargetPosition(Vector3 position, Vector3 targetPosition)
    { return (position - _LastPosition).sqrMagnitude >= (targetPosition - _LastPosition).sqrMagnitude; }
}
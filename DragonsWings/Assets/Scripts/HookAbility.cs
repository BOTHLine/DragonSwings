using UnityEngine;

public class HookAbility : MonoBehaviour
{
    // Components
    private AbilityManager _AbilityManager;
    private Hook _Hook;

    // References
    public FloatReference _HookRange;
    public FloatReference _HookSpeed;

    public FloatReference _PullSpeed;

    public FloatReference _Damage;

    public Vector2ComplexReference _Aim;

    public Vector2Reference _TargetDirection;
    public Vector2Reference _TargetPosition;

    public HookResponderVariable _AttachedHookResponder;

    // Events
    public GameEvent OnHookShoot;
    public GameEvent OnHookHit;
    public GameEvent OnHookReset;

    public GameEvent OnPullStart;
    public GameEvent OnPullFinish;

    public GameEvent OnObjectPickUp;

    // Variables
    private bool _HookIsFlying;

    // Mono Behaviour
    private void Awake()
    {
        _AbilityManager = GetComponentInParent<AbilityManager>();
        _Hook = GetComponentInChildren<Hook>();
        _Hook.Initialize(this, _HookSpeed);
    }

    private void FixedUpdate()
    {
        if (_HookIsFlying && ((Vector2)_Hook.transform.position).SquaredDistanceTo(transform.position) >= _HookRange * _HookRange)
        { _Hook.FlyBack(); }
    }

    // Methods
    public void ShootHook()
    {
        if (_HookIsFlying || _TargetDirection.Value.Equals(Vector2.zero)) { return; }
        // Vector2Complex aim = _Aim.Value;
        // if (_HookIsFlying || aim.Direction.Equals(Vector2.zero)) { return; }

        _HookIsFlying = true;
        _Hook.Shoot(_TargetPosition.Value);
        //_Hook.Shoot(aim.EndPoint);
        OnHookShoot.Raise();
    }

    public void HookHitSomething(Collider2D collider)
    {
        OnHookHit.Raise();
        HookResponder hookResponder = collider.GetComponentInSiblings<HookResponder>();
        if (hookResponder != null)
        {
            hookResponder.HitByHook();
            switch (hookResponder._Weight)
            {
                case Weight.None:
                    _Hook.ResetVelocity();
                    break;
                case Weight.Light:
                    _Hook.AttachHookResponder(hookResponder);
                    _Hook.FlyBack();
                    break;
                case Weight.Medium:
                    _PullSpeed.Value = _HookSpeed / 2.0f;
                    _Hook.AttachHookResponder(hookResponder);
                    OnPullStart.Raise();
                    _Hook.FlyBack();
                    break;
                case Weight.Heavy:
                    _PullSpeed.Value = _HookSpeed;
                    _Hook.StopHook();
                    OnPullStart.Raise();
                    break;
            }
        }
        else { _Hook.FlyBack(); }
    }

    public void HookReachedPlayer()
    {
        OnPullFinish.Raise();
        ResetHook();
        PickUpHookResponder();
    }

    private void PickUpHookResponder()
    {
        HookResponder hookResponder = _Hook.DetachHookResponder();
        if (hookResponder == null) return;

        switch (hookResponder._Weight)
        {
            case Weight.None:
                break;
            case Weight.Light:
                _AttachedHookResponder.Value = hookResponder;
                OnObjectPickUp.Raise();
                break;
            case Weight.Medium:
                break;
            case Weight.Heavy:
                break;
        }
    }

    public void ResetHook()
    {
        _HookIsFlying = false;

        _Hook.Reset();
        OnHookReset.Raise();
    }
}
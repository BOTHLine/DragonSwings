using UnityEngine;

public class HookAbility : MonoBehaviour
{
    // Components
    private Hook _Hook;

    // References
    public FloatReference _HookRange;
    public FloatReference _HookSpeed;

    public FloatReference _PullSpeed;

    public FloatReference _Damage;

    public Vector3ComplexReference _Aim;

    // public Vector2Reference _TargetDirection;
    // public Vector2Reference _TargetPosition;

    public HookResponderVariable _AttachedHookResponder;

    // Events
    public GameEvent _OnHookShoot;

    public GameEvent _OnHookHit;
    public GameEvent _OnHookReset;

    public GameEvent _OnPullStartRaise;
    public GameEvent _OnPullFinishRaise;

    public GameEvent _OnPickUpRaise;

    // Variables
    private bool _HookIsFlying;

    // Mono Behaviour
    private void Awake()
    {
        _Hook = GetComponentInChildren<Hook>();
        _Hook.Initialize(this, _HookSpeed.Value);
    }

    private void FixedUpdate()
    {
        if (_HookIsFlying && (_Hook.transform.position).SquaredDistanceTo(transform.position) >= _HookRange.Value * _HookRange.Value)
        { _Hook.FlyBack(); }
    }

    // Methods
    public void ShootHook()
    {
        // if (_HookIsFlying || _TargetDirection.Value.Equals(Vector2.zero)) { return; }
        if (_HookIsFlying) { return; }
        if (_Aim.Value.Direction.Equals(Vector3.zero)) { return; }
        _OnHookShoot.Raise();

        _HookIsFlying = true;
        //_Hook.Shoot(_TargetPosition.Value);
        _Hook.Shoot(_Aim.Value);
    }

    public void HookHitSomething(Collider collider)
    {
        _OnHookHit.Raise();
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
                    _PullSpeed.Value = _HookSpeed.Value / 2.0f;
                    _Hook.AttachHookResponder(hookResponder);
                    _OnPullStartRaise.Raise();
                    _Hook.FlyBack();
                    break;
                case Weight.Heavy:
                    _PullSpeed.Value = _HookSpeed.Value;
                    _Hook.StopHook();
                    _OnPullStartRaise.Raise();
                    break;
            }
        }
        else { _Hook.FlyBack(); }
    }

    public void HookReachedPlayer()
    {
        //   _OnPullFinishRaise.Raise();
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
                _OnPickUpRaise.Raise();
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
        _OnHookReset.Raise();
    }
}
using UnityEngine;

public class HookAbility : MonoBehaviour
{
    // Components
    public Hook _Hook;

    // References
    public FloatReference _HookRange;
    public FloatReference _HookSpeed;

    public FloatReference _Damage;

    public Vector2Reference _TargetPosition;

    // Events
    public GameEvent OnHookShoot;
    public GameEvent OnHookHit;
    public GameEvent OnHookReset;

    // Variables
    private bool _HookIsFlying;
    private HookResponder _AttachedHookResponder;

    // Mono Behaviour
    private void Awake()
    {
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
        if (_HookIsFlying || _TargetPosition.Value.Equals(transform.position)) { return; }

        if (_AttachedHookResponder == null)
        {
            _HookIsFlying = true;
            _Hook.Shoot(_TargetPosition.Value);
            OnHookShoot.Raise();
        }
        else
        {
            // Throw;
        }
    }

    public void HookHitSomething(Collider2D collision)
    {
        HookResponder hookResponder = collision.GetComponentInSiblings<HookResponder>();
        if (hookResponder != null)
        {
            hookResponder.HitByHook(_Hook);
            switch (hookResponder._Weight)
            {
                case Weight.None:
                    break;
                case Weight.Light:
                    AttachHookResponder(hookResponder);
                    _Hook.FlyBack();
                    break;
                case Weight.Medium:
                    _Hook.FlyBack();
                    break;
                case Weight.Heavy:
                    break;
            }
        }
        OnHookHit.Raise();
    }

    public void HookReachedPlayer()
    {
        ResetHook();
    }

    public void ResetHook()
    {
        _HookIsFlying = false;

        _Hook.Reset();
        OnHookReset.Raise();
    }

    private void AttachHookResponder(HookResponder hookResponder)
    {
        _AttachedHookResponder = hookResponder;
        hookResponder.AttachToObject(_Hook.transform);
    }

    private void ThrowHookResponder()
    {
        _AttachedHookResponder?.DetachFromObject();
        _AttachedHookResponder = null;
    }
}
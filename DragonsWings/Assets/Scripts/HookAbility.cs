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
    public GameEvent OnHookReset;

    // Variables
    private bool hookIsFlying;
    private bool hasSomethingToThrow;

    // Mono Behaviour
    private void FixedUpdate()
    {
        if (_TargetPosition.Value.SquaredDistanceTo(transform.position) >= _HookRange * _HookRange)
        { ResetHook(); }
    }

    // Methods
    public void ShootHook()
    {
        if (hookIsFlying) { return; }

        hookIsFlying = true;

        _Hook.Shoot(this, _TargetPosition, _HookSpeed);

        OnHookShoot.Raise();
    }

    public void HookHitSomething(Collision2D collision)
    {
        HookInteraction hookInteraction = collision.collider.GetComponentInSiblings<HookInteraction>();
        if (hookInteraction != null)
        {
            hookInteraction.HitByHook(_Hook);
            switch (hookInteraction._Weight)
            {
                case Weight.None:
                    break;
                case Weight.Light:
                    break;
                case Weight.Medium:
                    break;
                case Weight.Heavy:
                    break;
            }
        }
    }

    public void ResetHook()
    {
        OnHookReset.Raise();

        hookIsFlying = false;

        _Hook.Reset();
    }
}
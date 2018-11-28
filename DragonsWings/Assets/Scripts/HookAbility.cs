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
    private bool hookIsFlying;
    private bool hasSomethingToThrow;

    // Mono Behaviour
    private void FixedUpdate()
    {
        if (hookIsFlying && ((Vector2)_Hook.transform.position).SquaredDistanceTo(transform.position) >= _HookRange * _HookRange)
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
        HookResponder hookInteraction = collision.collider.GetComponentInSiblings<HookResponder>();
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
        OnHookHit.Raise();
    }

    public void ResetHook()
    {
        hookIsFlying = false;

        _Hook.Reset();
        OnHookReset.Raise();
    }
}
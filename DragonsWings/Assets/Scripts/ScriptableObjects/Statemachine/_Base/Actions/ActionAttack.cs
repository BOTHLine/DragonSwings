using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Attack")]
public class ActionAttack : Action
{
    // public BoolReference _IsAttackStarting;
    // public BoolReference _IsAttacking;
    public BoolMap _IsAttackStartingMap;
    public BoolMap _IsAttackingMap;

    // public FloatReference _CurrentAttackCooldown;
    public FloatMap _CurrentAttackCoolDownMap;
    public FloatReference _TotalAttackCooldown;

    public override void Act(StateController controller)
    { }

    /*
    public override void EnterState(StateController controller)
    {
        _IsAttackStartingMap.Set(controller.gameObject, true);
    }

    public override void ExitState(StateController controller)
    {
        _IsAttackingMap.Set(controller.gameObject, true);
        _CurrentAttackCoolDownMap.Set(controller.gameObject, _TotalAttackCooldown.Value);
    }
    */
}
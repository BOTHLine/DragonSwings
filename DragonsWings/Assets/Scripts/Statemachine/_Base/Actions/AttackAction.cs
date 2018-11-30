using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Attack Action")]
public class AttackAction : Action
{
    public BoolReference _IsAttackStarting;
    public BoolReference _IsAttacking;

    public override void Act(StateController controller)
    { }

    public override void EnterState(StateController controller)
    { _IsAttackStarting.Set(true, controller.gameObject); }

    public override void ExitState(StateController controller)
    { _IsAttacking.Set(true, controller.gameObject); }
}
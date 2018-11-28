using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Attack Action")]
public class AttackAction : Action
{
    public FloatReference _AttackCooldownTotal;
    public FloatReference _AttackCooldownCurrent;

    public BoolReference _IsAttacking;
    public BoolReference _IsAttackStarting;

    public GameEvent OnEnemyAttackStart;
    public GameEvent OnEnemyAttack;

    public override void Act(StateController controller)
    { }

    public override void EnterState(StateController controller)
    {
        //   OnEnemyAttackStart.Raise();
        _IsAttackStarting.Set(true, controller.gameObject);
        _AttackCooldownCurrent.Set(_AttackCooldownTotal.Get(controller.gameObject), controller.gameObject);
    }

    public override void ExitState(StateController controller)
    {
        _IsAttacking.Set(true, controller.gameObject);
        // OnEnemyAttack.Raise(); 
    }
}
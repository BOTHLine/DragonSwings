using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Attack Action")]
public class AttackAction : Action
{
    public GameEvent OnEnemyAttackStart;
    public GameEvent OnEnemyAttack;

    public override void Act(StateController controller)
    {

    }

    public override void EnterState(StateController controller)
    { OnEnemyAttackStart.Raise(); }

    public override void ExitState(StateController controller)
    { OnEnemyAttack.Raise(); }
}
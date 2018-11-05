using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Attack Action")]
public class AttackAction : Action
{
    public FloatReference currentAttackTime;

    public GameEvent OnEnemyAttack;

    public override void Act(StateController controller)
    {
        currentAttackTime.Variable.Value += Time.deltaTime;
    }

    public override void EnterState(StateController controller)
    {
        currentAttackTime.Variable.Value = 0.0f;
    }

    public override void ExitState(StateController controller)
    {
        OnEnemyAttack.Raise();
    }
}
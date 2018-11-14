using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Bool Decision")]
public class BoolDecision : Decision
{
    public BoolReference value;
    public CompareOperator compareOperator;
    public BoolReference compareValue;

    public override bool Decide(StateController controller)
    {
        switch (compareOperator)
        {
            case CompareOperator.Equals: return value == compareValue;
            case CompareOperator.NotEquals: return value != compareValue;
            case CompareOperator.Less: return value < compareValue;
            case CompareOperator.Greater: return value > compareValue;
            case CompareOperator.LessEquals: return value <= compareValue;
            case CompareOperator.GreaterEquals: return value >= compareValue;
        }
        return false;
    }

    public override void EnterState(StateController controller)
    {
        value.SetEmptyMapIdentifier(controller.gameObject);
        compareValue.SetEmptyMapIdentifier(controller.gameObject);
    }

    public override void ExitState(StateController controller)
    { }
}
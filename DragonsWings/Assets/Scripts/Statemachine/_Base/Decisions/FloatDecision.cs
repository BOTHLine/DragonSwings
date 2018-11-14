using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Float Decision")]
public class FloatDecision : Decision
{
    public FloatReference value;
    public CompareOperator compareOperator;
    public FloatReference compareValue;

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

    public override void ExitState(StateController controller) { }
}
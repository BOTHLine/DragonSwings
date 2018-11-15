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
            case CompareOperator.Equals: return value.Get(controller.gameObject) == compareValue.Get(controller.gameObject);
            case CompareOperator.NotEquals: return value.Get(controller.gameObject) != compareValue.Get(controller.gameObject);
            case CompareOperator.Less: return value.Get(controller.gameObject) < compareValue.Get(controller.gameObject);
            case CompareOperator.Greater: return value.Get(controller.gameObject) > compareValue.Get(controller.gameObject);
            case CompareOperator.LessEquals: return value.Get(controller.gameObject) <= compareValue.Get(controller.gameObject);
            case CompareOperator.GreaterEquals: return value.Get(controller.gameObject) >= compareValue.Get(controller.gameObject);
        }
        return false;
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Bool")]
public class DecisionBool : Decision
{
    public BoolReference value;
    public CompareOperator compareOperator;
    public BoolReference compareValue;

    public bool currentValue;

    public override bool Decide(StateController controller)
    {
        switch (compareOperator)
        {
            case CompareOperator.Equals: return value.Get(controller.gameObject) == compareValue.Get(controller.gameObject);
            case CompareOperator.NotEquals: return value.Get(controller.gameObject) != compareValue.Get(controller.gameObject);
            case CompareOperator.Less: return !value.Get(controller.gameObject) && compareValue.Get(controller.gameObject);
            case CompareOperator.Greater: return value.Get(controller.gameObject) && !compareValue.Get(controller.gameObject);
            case CompareOperator.LessEquals: return value.Get(controller.gameObject) == compareValue.Get(controller.gameObject) || (!value.Get(controller.gameObject) && compareValue.Get(controller.gameObject));
            case CompareOperator.GreaterEquals: return value.Get(controller.gameObject) == compareValue.Get(controller.gameObject) || (value.Get(controller.gameObject) && !compareValue.Get(controller.gameObject));
        }
        return false;
    }
}
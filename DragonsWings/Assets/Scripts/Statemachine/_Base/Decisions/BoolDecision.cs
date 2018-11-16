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
            case CompareOperator.Equals: return value.Get(controller.gameObject) == compareValue.Get(controller.gameObject);
            case CompareOperator.NotEquals: return value.Get(controller.gameObject) != compareValue.Get(controller.gameObject);
            case CompareOperator.Less: return !value.Get(controller.gameObject) && compareValue.Get(controller.gameObject);
            case CompareOperator.Greater: return value.Get(controller.gameObject) && !compareValue.Get(controller.gameObject);
            case CompareOperator.LessEquals: return value.Get(controller.gameObject) == compareValue.Get(controller.gameObject) || (!value.Get(controller.gameObject) && compareValue.Get(controller.gameObject));
            case CompareOperator.GreaterEquals: return value.Get(controller.gameObject) == compareValue.Get(controller.gameObject) || (value.Get(controller.gameObject) && !compareValue.Get(controller.gameObject));
        }
        return false;
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }
}
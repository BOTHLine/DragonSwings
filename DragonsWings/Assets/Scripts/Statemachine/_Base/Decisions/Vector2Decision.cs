using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Vector2 Decision")]
public class Vector2Decision : Decision
{
    public Vector2Reference value;
    public CompareOperator compareOperator;
    public Vector2Reference compareValue;

    public override bool Decide(StateController controller)
    {
        switch (compareOperator)
        {
            case CompareOperator.Equals: return value.Get(controller.gameObject) == compareValue.Get(controller.gameObject);
            case CompareOperator.NotEquals: return value.Get(controller.gameObject) != compareValue.Get(controller.gameObject);
        }
        return false;
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }
}
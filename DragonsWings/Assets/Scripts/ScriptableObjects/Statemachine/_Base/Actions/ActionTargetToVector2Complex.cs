using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Target To Vector2Complex")]
public class ActionTargetToVector2Complex : Action
{
    public Vector2Reference _TargetPosition;
    public Vector2ComplexMap _MoveMap;

    public override void Act(StateController controller)
    { _MoveMap.Set(controller.gameObject, new Vector2Complex(controller.transform.position, _TargetPosition.Get(controller.gameObject))); }
}
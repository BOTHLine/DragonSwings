using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Target To Vector2Complex Invert")]
public class ActionTargetToVector2ComplexInvert : Action
{
    public Vector2Reference _TargetPosition;
    public Vector2ComplexMap _MoveMap;

    public override void Act(StateController controller)
    { _MoveMap.Set(controller.gameObject, new Vector2Complex(_TargetPosition.Get(controller.gameObject), controller.transform.position)); }
}
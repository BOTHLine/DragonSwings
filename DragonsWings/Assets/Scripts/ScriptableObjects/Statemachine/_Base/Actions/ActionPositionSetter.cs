using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/PositionSetter")]
public class ActionPositionSetter : Action
{
    public Vector2Map _PositionMap;

    public override void Act(StateController controller)
    { controller.transform.position = _PositionMap.Get(controller.gameObject); }
}
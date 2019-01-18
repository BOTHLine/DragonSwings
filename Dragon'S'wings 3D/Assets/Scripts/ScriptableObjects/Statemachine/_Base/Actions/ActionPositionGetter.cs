using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Position Getter")]
public class ActionPositionGetter : Action
{
    public Vector3Map _PositionMap;

    public override void Act(StateController controller)
    { _PositionMap.Set(controller.gameObject, controller.transform.position); }
}
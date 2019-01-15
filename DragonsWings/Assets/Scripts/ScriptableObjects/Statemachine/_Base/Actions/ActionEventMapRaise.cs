using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/EventMap Raise")]

public class ActionEventMapRaise : Action
{
    public GameEventMap _GameEventMap;

    public override void Act(StateController controller)
    { _GameEventMap.Raise(controller.gameObject); }
}
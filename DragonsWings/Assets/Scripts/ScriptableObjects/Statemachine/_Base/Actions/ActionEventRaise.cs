using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Event Raise")]
public class ActionEventRaise : Action
{
    public GameEvent _GameEvent;

    public override void Act(StateController controller)
    { _GameEvent.Raise(); }
}
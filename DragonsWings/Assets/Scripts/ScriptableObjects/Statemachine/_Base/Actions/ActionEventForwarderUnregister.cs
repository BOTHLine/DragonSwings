using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/EventForwarder Unregister")]
public class ActionEventForwarderUnregister : Action
{
    public GameEvent _ListenEvent;
    public GameEvent _RaiseEvent;

    public override void Act(StateController controller)
    { _ListenEvent.UnregisterListener(_RaiseEvent.Raise); }
}
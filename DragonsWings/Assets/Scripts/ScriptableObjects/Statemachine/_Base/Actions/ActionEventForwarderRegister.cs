using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/EventForwarder Register")]
public class ActionEventForwarderRegister : Action
{
    public GameEvent _ListenEvent;
    public GameEvent _RaiseEvent;

    public override void Act(StateController controller)
    { _ListenEvent.RegisterListener(_RaiseEvent.Raise); }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Event")]
public class DecisionEvent : Decision
{
    private bool transition;
    private UnityEngine.Events.UnityEvent response;

    public GameEvent listenEvent;

    public override bool Decide(StateController controller) { return transition; }

    public override void EnterState(StateController controller)
    {
        transition = false;
        response = new UnityEngine.Events.UnityEvent();
        response.AddListener(SetTrue);
        listenEvent.RegisterListener(response);
    }

    public override void ExitState(StateController controller) { listenEvent.UnregisterListener(response); }
    public void SetTrue() { transition = true; }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/EventMap")]
public class DecisionEventMap : Decision
{
    private bool transition;
    private UnityEngine.Events.UnityEvent response;

    public GameEventMap listenEvent;

    public override bool Decide(StateController controller) { return transition; }

    public override void EnterState(StateController controller)
    {
        transition = false;
        response = new UnityEngine.Events.UnityEvent();
        response.AddListener(SetTrue);
        Debug.Log("Register: " + controller.gameObject + " on EventMap: " + listenEvent.name);
        listenEvent.RegisterListener(controller.gameObject, response);
    }

    public override void ExitState(StateController controller)
    { listenEvent.UnregisterListener(controller.gameObject, response); }

    public void SetTrue() { transition = true; Debug.Log("Transition True"); }
}
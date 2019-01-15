using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEngine.Events.UnityEvent Response;

    private void OnEnable() { Event.RegisterListener(Response); }
    private void OnDisable() { Event.UnregisterListener(Response); }
    public void OnEventRaised() { Response.Invoke(); }
}
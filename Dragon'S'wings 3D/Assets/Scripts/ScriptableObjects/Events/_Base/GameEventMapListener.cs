using UnityEngine;

public class GameEventMapListener : MonoBehaviour
{
    public GameEventMap _Event;
    public GameObject _Key;
    public UnityEngine.Events.UnityEvent _Response;

    private void OnEnable() { _Event.RegisterListener(_Key, _Response); }
    private void OnDisable() { _Event.UnregisterListener(_Key, _Response); }
    public void OnEventRaised() { _Response.Invoke(); }
}
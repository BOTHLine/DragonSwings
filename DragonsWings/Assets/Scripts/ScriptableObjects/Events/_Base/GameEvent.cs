using UnityEngine;

[CreateAssetMenu(menuName = "Events/Game Event", fileName = "New Game Event")]
public class GameEvent : ScriptableObject
{
    private System.Collections.Generic.List<UnityEngine.Events.UnityEvent> listener = new System.Collections.Generic.List<UnityEngine.Events.UnityEvent>();
    //   private System.Collections.Generic.List<GameEventListener> Listeners = new System.Collections.Generic.List<GameEventListener>();

    public void Raise()
    {
        for (int i = listener.Count - 1; i >= 0; i--)
        {
            listener[i].Invoke();
        }
    }

    public void RegisterListener(UnityEngine.Events.UnityEvent _listener)
    { if (!listener.Contains(_listener)) listener.Add(_listener); }

    public void UnregisterListener(UnityEngine.Events.UnityEvent _listener)
    { if (listener.Contains(_listener)) listener.Remove(_listener); }

    /*
    public void Raise()
    {
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    { if (!Listeners.Contains(listener)) Listeners.Add(listener); }

    public void UnregisterListener(GameEventListener listener)
    { if (Listeners.Contains(listener)) Listeners.Remove(listener); }
    */

    public void OnAfterDeserialize() { }
    public void OnBeforeSerialize() { }
}

[UnityEditor.CustomEditor(typeof(GameEvent))]
public class GameEventEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameEvent e = target as GameEvent;
        if (GUILayout.Button("Raise"))
            e.Raise();
    }
}

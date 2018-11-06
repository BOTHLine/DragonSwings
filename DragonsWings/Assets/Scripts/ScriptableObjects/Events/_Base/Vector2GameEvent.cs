using UnityEngine;

[CreateAssetMenu(menuName = "Events/Vector2 Game Event", fileName = "New Vector2 Game Event")]
public class Vector2GameEvent : ScriptableObject
{
    private System.Collections.Generic.List<UnityEngine.Events.UnityEvent<Vector2>> listener = new System.Collections.Generic.List<UnityEngine.Events.UnityEvent<Vector2>>();
    //   private System.Collections.Generic.List<GameEventListener> Listeners = new System.Collections.Generic.List<GameEventListener>();

    public void Raise(Vector2 vector2)
    {
        for (int i = listener.Count - 1; i >= 0; i--)
        {
            listener[i].Invoke(vector2);
        }
    }

    public void RegisterListener(UnityEngine.Events.UnityEvent<Vector2> _listener)
    { if (!listener.Contains(_listener)) listener.Add(_listener); }

    public void UnregisterListener(UnityEngine.Events.UnityEvent<Vector2> _listener)
    { if (listener.Contains(_listener)) listener.Remove(_listener); }

    public void OnAfterDeserialize() { }
    public void OnBeforeSerialize() { }
}

[UnityEditor.CustomEditor(typeof(Vector2GameEvent))]
public class Vector2GameEventEditor : UnityEditor.Editor
{
    private Vector2 vector2;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        Vector2GameEvent e = target as Vector2GameEvent;

        vector2 = UnityEditor.EditorGUILayout.Vector2Field("Paremeter:", vector2);

        if (GUILayout.Button("Raise"))
        { e.Raise(vector2); }
    }
}
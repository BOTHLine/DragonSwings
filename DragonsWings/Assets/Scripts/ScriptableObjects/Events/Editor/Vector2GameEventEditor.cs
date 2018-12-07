using UnityEngine;

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
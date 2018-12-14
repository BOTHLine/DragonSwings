using UnityEngine;

[UnityEditor.CustomEditor(typeof(GameEventMap))]
public class GameEventMapEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameEventMap e = target as GameEventMap;
        if (GUILayout.Button("Raise All"))
            e.RaiseAll();
    }
}


using UnityEngine;

[UnityEditor.CustomEditor(typeof(WeightGameEvent))]
public class WeightGameEventEditor : UnityEditor.Editor
{
    private Weight weight;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        WeightGameEvent e = target as WeightGameEvent;

        weight = (Weight)UnityEditor.EditorGUILayout.EnumPopup("Parameter:", weight);

        if (GUILayout.Button("Raise"))
        { e.Raise(weight); }
    }
}
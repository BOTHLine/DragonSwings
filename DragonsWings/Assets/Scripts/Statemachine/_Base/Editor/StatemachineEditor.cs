using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(State))]
public class StatemachineEditorWindow : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //   EditorGUILayout.ObjectField()
    }
}
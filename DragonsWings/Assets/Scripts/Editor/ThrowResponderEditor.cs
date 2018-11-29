using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ThrowResponder))]
public class ThrowResponderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ThrowResponder instance = target as ThrowResponder;

        EditorGUILayout.BeginHorizontal();
        {
            instance._AutoAim = GUILayout.Toggle(instance._AutoAim, "Auto Aim");

            if (instance._AutoAim)
            { instance._AutoAimPriority = EditorGUILayout.IntField("Priority:", instance._AutoAimPriority); }
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }
}

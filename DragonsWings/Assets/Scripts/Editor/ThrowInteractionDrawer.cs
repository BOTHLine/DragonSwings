using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ThrowInteraction))]
public class ThrowInteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ThrowInteraction instance = target as ThrowInteraction;

        EditorGUILayout.BeginHorizontal();
        {
            instance._AutoAim = GUILayout.Toggle(instance._AutoAim, "Auto Aim");

            if (instance._AutoAim)
            { instance._AutoAimPriority = EditorGUILayout.IntField("Priority:", instance._AutoAimPriority); }
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_OnHitByThrowable"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_OnHitByThrowableUnityEvent"), true);
        serializedObject.ApplyModifiedProperties();
    }
}

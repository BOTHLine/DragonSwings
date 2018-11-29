using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HookResponder))]
public class HookResponderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HookResponder instance = target as HookResponder;

        EditorGUILayout.BeginHorizontal();
        {
            instance._AutoAim = GUILayout.Toggle(instance._AutoAim, "Auto Aim");

            if (instance._AutoAim)
            { instance._AutoAimPriority = EditorGUILayout.IntField("Priority:", instance._AutoAimPriority); }
        }
        EditorGUILayout.EndHorizontal();

        instance._Weight = (Weight)EditorGUILayout.EnumPopup("Weight:", instance._Weight);

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("OnHitByHookUnityEvent"), true);
        if (instance._Weight == Weight.Light || instance._Weight == Weight.None)
        { EditorGUILayout.PropertyField(serializedObject.FindProperty("OnObjectLanded"), true); }
        serializedObject.ApplyModifiedProperties();
    }
}
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HookResponder))]
public class HookResponderDrawer : Editor
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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_OnHitByHook"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_OnHitByHookUnityEvent"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
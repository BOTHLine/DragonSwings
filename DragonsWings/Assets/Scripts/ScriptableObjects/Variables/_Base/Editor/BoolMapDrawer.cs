/*
using UnityEngine;

[UnityEditor.CustomEditor(typeof(BoolMap))]
public class BoolMapEditor : UnityEditor.Editor
{
    /// <summary>
    /// Options to display in the popup to select constant or variable.
    /// </summary>

    /// <summary> Cached style to use to draw the popup button. </summary>
    private GUIStyle popupStyle;

    private BoolMap boolMap;

    public override void OnInspectorGUI()
    {
        for (int i = 0; i < boolMap.KeyNames.Length; i++)
        {
            UnityEditor.EditorGUILayout.BeginHorizontal();

            UnityEditor.EditorGUILayout.TextField(boolMap.KeyNames[i]);
            UnityEditor.EditorGUILayout.Toggle(boolMap.Values[i]);

            UnityEditor.EditorGUILayout.EndHorizontal();
        }
    }

    private void OnEnable()
    {
        boolMap = target as BoolMap;
    }

    /*
    public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        Debug.Log("Test");
        if (popupStyle == null)
        {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        label = UnityEditor.EditorGUI.BeginProperty(position, label, property);
        position = UnityEditor.EditorGUI.PrefixLabel(position, label);

        UnityEditor.EditorGUI.BeginChangeCheck();

        // Get properties
        UnityEditor.SerializedProperty keys = property.FindPropertyRelative("Keys");
        UnityEditor.SerializedProperty values = property.FindPropertyRelative("Values");
        UnityEditor.SerializedProperty items = property.FindPropertyRelative("Items");

        Debug.Log(items);
        Debug.Log(keys);
        Debug.Log(values);

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = UnityEditor.EditorGUI.indentLevel;
        UnityEditor.EditorGUI.indentLevel = 0;

        float width = (position.xMax - position.xMin) / 2.0f;
        Rect positionSecond = position;

        position.width = width;
        positionSecond.xMin = position.xMax;

        Debug.Log(keys.isArray + ": " + keys.arraySize);
        for (int i = 0; i < keys.arraySize; i++)
        {
            UnityEditor.EditorGUI.TextArea(position, keys.GetArrayElementAtIndex(i).name);
            //   UnityEditor.EditorGUI.PropertyField(position, keys.GetArrayElementAtIndex(i).name, GUIContent.none);
            UnityEditor.EditorGUI.PropertyField(positionSecond, values.GetArrayElementAtIndex(i), GUIContent.none);
            position.y = positionSecond.y = position.y + 16;
        }

        if (UnityEditor.EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        UnityEditor.EditorGUI.indentLevel = indent;
        UnityEditor.EditorGUI.EndProperty();
    }
}
*/
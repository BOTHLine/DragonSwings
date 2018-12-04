using UnityEngine;

[UnityEditor.CustomPropertyDrawer(typeof(HookResponderReference))]
public class HookResponderReferenceDrawer : UnityEditor.PropertyDrawer
{
    /// <summary>
    /// Options to display in the popup to select constant or variable.
    /// </summary>
    private readonly string[] popupOptions =
        { "Use Constant", "Use Variable", "Use Set"};

    /// <summary> Cached style to use to draw the popup button. </summary>
    private GUIStyle popupStyle;

    public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        if (popupStyle == null)
        {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        label = UnityEditor.EditorGUI.BeginProperty(position, label, property);
        position = UnityEditor.EditorGUI.PrefixLabel(position, label);

        UnityEditor.EditorGUI.BeginChangeCheck();

        // Get properties
        UnityEditor.SerializedProperty useType = property.FindPropertyRelative("UseType");
        UnityEditor.SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
        UnityEditor.SerializedProperty variable = property.FindPropertyRelative("Variable");
        UnityEditor.SerializedProperty set = property.FindPropertyRelative("Map");
        UnityEditor.SerializedProperty setIdentifier = property.FindPropertyRelative("MapIdentifier");

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = UnityEditor.EditorGUI.indentLevel;
        UnityEditor.EditorGUI.indentLevel = 0;

        int result = UnityEditor.EditorGUI.Popup(buttonRect, useType.intValue, popupOptions, popupStyle);

        useType.intValue = result;

        switch (useType.intValue)
        {
            case (int)ReferenceUseType.Constant:
                UnityEditor.EditorGUI.PropertyField(position, constantValue, GUIContent.none);
                break;
            case (int)ReferenceUseType.Variable:
                UnityEditor.EditorGUI.PropertyField(position, variable, GUIContent.none);
                break;
            case (int)ReferenceUseType.Map:
                float width = (position.xMax - position.xMin) / 2.0f;
                Rect positionSecond = position;

                position.width = width;
                UnityEditor.EditorGUI.PropertyField(position, setIdentifier, GUIContent.none);

                positionSecond.xMin = position.xMax;
                UnityEditor.EditorGUI.PropertyField(positionSecond, set, GUIContent.none);
                break;
        }

        if (UnityEditor.EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        UnityEditor.EditorGUI.indentLevel = indent;
        UnityEditor.EditorGUI.EndProperty();
    }
}
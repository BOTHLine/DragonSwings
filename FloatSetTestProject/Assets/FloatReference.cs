using UnityEngine;

[System.Serializable]
public class FloatReference : BaseReference<FloatVariable, FloatSet, float>
{
    public static bool operator ==(FloatReference left, FloatReference right) { return left.Value == right.Value; }
    public static bool operator !=(FloatReference left, FloatReference right) { return left.Value != right.Value; }
    public static bool operator <(FloatReference left, FloatReference right) { return left.Value < right.Value; }
    public static bool operator >(FloatReference left, FloatReference right) { return left.Value > right.Value; }
    public static bool operator <=(FloatReference left, FloatReference right) { return left.Value <= right.Value; }
    public static bool operator >=(FloatReference left, FloatReference right) { return left.Value >= right.Value; }

    public static float operator +(FloatReference left, FloatReference right) { return left.Value + right.Value; }
    public static float operator -(FloatReference left, FloatReference right) { return left.Value - right.Value; }
    public static float operator *(FloatReference left, FloatReference right) { return left.Value * right.Value; }
    public static float operator /(FloatReference left, FloatReference right) { return left.Value / right.Value; }

    public static FloatReference operator --(FloatReference instance) { instance.Value--; return instance; }
    public static FloatReference operator ++(FloatReference instance) { instance.Value++; return instance; }

    public override bool Equals(object obj) { return base.Equals(obj); }
    public override int GetHashCode() { return base.GetHashCode(); }
}

[UnityEditor.CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferenceDrawer : UnityEditor.PropertyDrawer
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
        UnityEditor.SerializedProperty useType = property.FindPropertyRelative("useType");
        UnityEditor.SerializedProperty constantValue = property.FindPropertyRelative("constantValue");
        UnityEditor.SerializedProperty variable = property.FindPropertyRelative("variable");
        UnityEditor.SerializedProperty set = property.FindPropertyRelative("set");
        UnityEditor.SerializedProperty setIdentifier = property.FindPropertyRelative("setIdentifier");

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
            case (int)UseType.Constant:
                UnityEditor.EditorGUI.PropertyField(position, constantValue, GUIContent.none);
                break;
            case (int)UseType.Variable:
                UnityEditor.EditorGUI.PropertyField(position, variable, GUIContent.none);
                break;
            case (int)UseType.Set:
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
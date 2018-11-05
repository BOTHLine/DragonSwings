using UnityEngine;

[System.Serializable]
public class FloatReference : BaseReference<FloatVariable, float>
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
        { "Use Constant", "Use Variable" };

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
        UnityEditor.SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
        UnityEditor.SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
        UnityEditor.SerializedProperty variable = property.FindPropertyRelative("Variable");

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = UnityEditor.EditorGUI.indentLevel;
        UnityEditor.EditorGUI.indentLevel = 0;

        int result = UnityEditor.EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

        useConstant.boolValue = result == 0;

        UnityEditor.EditorGUI.PropertyField(position,
            useConstant.boolValue ? constantValue : variable,
            GUIContent.none);

        if (UnityEditor.EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        UnityEditor.EditorGUI.indentLevel = indent;
        UnityEditor.EditorGUI.EndProperty();
    }
}
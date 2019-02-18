using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationLoader))]
public class AnimationLoaderEditor : Editor
{
    private int animationIndex;

    private string[] directions = { "Up", "Up Right", "Right", "Down Right", "Down", "Down Left", "Left", "Up Left" };
    private int directionIndex;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        if (!Application.isPlaying) return;

        AnimationLoader a = target as AnimationLoader;

        animationIndex = EditorGUILayout.Popup(animationIndex, a._Animations);
        directionIndex = EditorGUILayout.Popup(directionIndex, directions);

        if (GUILayout.Button("Load Animation"))
        {
            Vector2 direction;
            switch (directions[directionIndex])
            {
                case "Up": direction = Vector2.up; break;
                case "Up Right": direction = Vector2.up + Vector2.right; break;
                case "Right": direction = Vector2.right; break;
                case "Down Right": direction = Vector2.down + Vector2.right; break;
                case "Down": direction = Vector2.down; break;
                case "Down Left": direction = Vector2.down + Vector2.left; break;
                case "Left": direction = Vector2.left; break;
                case "Up Left": direction = Vector2.up + Vector2.left; break;
                default: direction = Vector2.down; break;
            }

            a.LoadAnimation(animationIndex, direction);
        }
    }
}
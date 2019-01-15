using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Variables/Vector2 Variable")]
public class Vector2Variable : BaseVariable<Vector2>
{
    public void ApplyChange(Vector2 amount) { Value += amount; }
    public void ApplyChange(Vector2Variable amount) { Value += amount.Value; }
}
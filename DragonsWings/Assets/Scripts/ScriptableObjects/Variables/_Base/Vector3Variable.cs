using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Vector3 Variable")]
public class Vector3Variable : BaseVariable<Vector3>
{
    public void ApplyChange(Vector3 amount) { Value += amount; }
    public void ApplyChange(Vector3Variable amount) { Value += amount.Value; }
}
using UnityEngine;

[CreateAssetMenu(menuName = "OverlapCollider2D/OverlapBoxCollider2D")]
public class OverlapBoxCollider2D : ScriptableObject
{
    public Vector2 size;

    public Collider2D OverlapCollider(Vector2 position, float angle)
    { return Physics2D.OverlapBox(position, size, angle); }

    public Collider2D[] OverlapColliderAll(Vector2 position, float angle)
    { return Physics2D.OverlapBoxAll(position, size, angle); }
}
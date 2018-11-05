using UnityEngine;

public abstract class OverlapCollider2D : ScriptableObject
{
    public abstract Collider2D OverlapCollider(Vector2 position);
    public abstract Collider2D[] OverlapColliderAll(Vector2 position);
}
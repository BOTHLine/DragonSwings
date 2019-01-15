using UnityEngine;

public static class TransformExtensions
{
    public static void LookAt2D(this Transform transform, Vector2 targetPosition, float rotation = 0.0f)
    { transform.rotation = Utils.GetLookAtRotation(transform.position, targetPosition, rotation); }
}
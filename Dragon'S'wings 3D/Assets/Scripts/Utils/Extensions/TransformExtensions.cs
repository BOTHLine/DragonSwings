using UnityEngine;

public static class TransformExtensions
{
    public static void LookAt2D(this Transform transform, Vector3 targetPosition, float rotation = 0.0f)
    { transform.rotation = Utils.GetLookAtRotation2D(transform.position, targetPosition, rotation); }
}
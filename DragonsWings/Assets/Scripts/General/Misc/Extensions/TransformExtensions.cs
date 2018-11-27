using UnityEngine;

public static class TransformExtensions
{
    public static void LookAt2D(this Transform transform, Vector2 targetPosition, float rotation = 0.0f)
    {
        Vector2 direction = targetPosition - (Vector2)transform.position;

        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, zRotation + rotation);
    }
}
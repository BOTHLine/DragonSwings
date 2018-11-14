using UnityEngine;

public static class Utils
{
    public static Quaternion GetLookAtRotation(Vector2 direction, float rotation = 0.0f)
    {
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0.0f, 0.0f, zRotation + rotation);
    }

    public static Quaternion GetLookAtRotation(Vector2 startPosition, Vector2 targetPosition, float rotation = 0.0f)
    {
        return GetLookAtRotation((targetPosition - startPosition).normalized, rotation);
    }
}
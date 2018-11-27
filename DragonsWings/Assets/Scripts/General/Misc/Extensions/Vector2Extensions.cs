using UnityEngine;

public static class Vector2Extensions
{
    public static float SquaredDistanceTo(this Vector2 vector2, Vector2 other)
    {
        return (vector2 - other).sqrMagnitude;
    }
}
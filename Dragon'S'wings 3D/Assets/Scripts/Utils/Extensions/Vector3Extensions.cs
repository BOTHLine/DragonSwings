using UnityEngine;

public static class Vector3Extensions
{
    public static float SquaredDistanceTo(this Vector3 vector3, Vector3 other)
    {
        return (vector3 - other).sqrMagnitude;
    }
}
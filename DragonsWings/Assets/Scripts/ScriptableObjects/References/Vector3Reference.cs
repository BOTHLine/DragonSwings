using UnityEngine;

[System.Serializable]
public class Vector3Reference : BaseReference<Vector3Variable, Vector3Map, Vector3>
{
    public static bool operator ==(Vector3Reference left, Vector3Reference right) { return left.Value.x == right.Value.x && left.Value.y == right.Value.y; }
    public static bool operator !=(Vector3Reference left, Vector3Reference right) { return left.Value.x != right.Value.x || left.Value.y != right.Value.y; }

    public static Vector3 operator +(Vector3Reference left, Vector3Reference right) { return left.Value + right.Value; }
    public static Vector3 operator -(Vector3Reference left, Vector3Reference right) { return left.Value - right.Value; }

    public override bool Equals(object obj) { return base.Equals(obj); }
    public override int GetHashCode() { return base.GetHashCode(); }

    public bool Equals(Vector3 vector2) { return Value.Equals(vector2); }
}
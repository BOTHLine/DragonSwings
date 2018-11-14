using UnityEngine;

[System.Serializable]
public class Vector2Reference : BaseReference<Vector2Variable, Vector2Map, Vector2>
{
    public static bool operator ==(Vector2Reference left, Vector2Reference right) { return left.Value.x == right.Value.x && left.Value.y == right.Value.y; }
    public static bool operator !=(Vector2Reference left, Vector2Reference right) { return left.Value.x != right.Value.x || left.Value.y != right.Value.y; }

    public static Vector2 operator +(Vector2Reference left, Vector2Reference right) { return left.Value + right.Value; }
    public static Vector2 operator -(Vector2Reference left, Vector2Reference right) { return left.Value - right.Value; }
    public static Vector2 operator *(Vector2Reference left, Vector2Reference right) { return left.Value * right.Value; }
    public static Vector2 operator /(Vector2Reference left, Vector2Reference right) { return left.Value / right.Value; }

    public override bool Equals(object obj) { return base.Equals(obj); }
    public override int GetHashCode() { return base.GetHashCode(); }
}
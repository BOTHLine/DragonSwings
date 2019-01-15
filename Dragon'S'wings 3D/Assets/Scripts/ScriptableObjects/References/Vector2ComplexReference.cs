using UnityEngine;

[System.Serializable]
public class Vector2ComplexReference : BaseReference<Vector2ComplexVariable, Vector2ComplexMap, Vector2Complex>
{
    // public static bool operator ==(Vector2ComplexReference left, Vector2ComplexReference right) { return left.Value.x == right.Value.x && left.Value.y == right.Value.y; }
    // public static bool operator !=(Vector2ComplexReference left, Vector2ComplexReference right) { return left.Value.x != right.Value.x || left.Value.y != right.Value.y; }

    // public static Vector2Complex operator +(Vector2ComplexReference left, Vector2ComplexReference right) { return left.Value + right.Value; }
    // public static Vector2Complex operator -(Vector2ComplexReference left, Vector2ComplexReference right) { return left.Value - right.Value; }
    // public static Vector2Complex operator *(Vector2ComplexReference left, Vector2ComplexReference right) { return left.Value * right.Value; }
    // public static Vector2Complex operator /(Vector2ComplexReference left, Vector2ComplexReference right) { return left.Value / right.Value; }

    public override bool Equals(object obj) { return base.Equals(obj); }
    public override int GetHashCode() { return base.GetHashCode(); }

    public bool Equals(Vector2Complex vector2) { return Value.Equals(vector2); }
}
using UnityEngine;

[System.Serializable]
public class Vector3ComplexReference : BaseReference<Vector3ComplexVariable, Vector3ComplexMap, Vector3Complex>
{
    // public static bool operator ==(Vector3ComplexReference left, Vector3ComplexReference right) { return left.Value.x == right.Value.x && left.Value.y == right.Value.y; }
    // public static bool operator !=(Vector3ComplexReference left, Vector3ComplexReference right) { return left.Value.x != right.Value.x || left.Value.y != right.Value.y; }

    // public static Vector3Complex operator +(Vector3ComplexReference left, Vector3ComplexReference right) { return left.Value + right.Value; }
    // public static Vector3Complex operator -(Vector3ComplexReference left, Vector3ComplexReference right) { return left.Value - right.Value; }
    // public static Vector3Complex operator *(Vector3ComplexReference left, Vector3ComplexReference right) { return left.Value * right.Value; }
    // public static Vector3Complex operator /(Vector3ComplexReference left, Vector3ComplexReference right) { return left.Value / right.Value; }

    public override bool Equals(object obj) { return base.Equals(obj); }
    public override int GetHashCode() { return base.GetHashCode(); }

    public bool Equals(Vector3Complex vector3) { return Value.Equals(vector3); }
}
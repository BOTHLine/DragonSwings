using UnityEngine;

[System.Serializable]
public class FloatReference : BaseReference<FloatVariable, FloatMap, float>
{
    public static bool operator ==(FloatReference left, FloatReference right) { return left.Value == right.Value; }
    public static bool operator !=(FloatReference left, FloatReference right) { return left.Value != right.Value; }
    public static bool operator <(FloatReference left, FloatReference right) { return left.Value < right.Value; }
    public static bool operator >(FloatReference left, FloatReference right) { return left.Value > right.Value; }
    public static bool operator <=(FloatReference left, FloatReference right) { return left.Value <= right.Value; }
    public static bool operator >=(FloatReference left, FloatReference right) { return left.Value >= right.Value; }

    public static float operator +(FloatReference left, FloatReference right) { return left.Value + right.Value; }
    public static float operator -(FloatReference left, FloatReference right) { return left.Value - right.Value; }
    public static float operator *(FloatReference left, FloatReference right) { return left.Value * right.Value; }
    public static float operator /(FloatReference left, FloatReference right) { return left.Value / right.Value; }

    public static FloatReference operator --(FloatReference instance) { instance.Value--; return instance; }
    public static FloatReference operator ++(FloatReference instance) { instance.Value++; return instance; }

    public override bool Equals(object obj) { return base.Equals(obj); }
    public override int GetHashCode() { return base.GetHashCode(); }
}
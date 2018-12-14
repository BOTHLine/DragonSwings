using UnityEngine;

[System.Serializable]
public class IntReference : BaseReference<IntVariable, IntMap, int>
{
    public void ChangeValue(int value, GameObject mapIdentifier) { Set(Get(mapIdentifier) + value, mapIdentifier); }

    public static bool operator ==(IntReference left, IntReference right) { return left.Value == right.Value; }
    public static bool operator !=(IntReference left, IntReference right) { return left.Value != right.Value; }
    public static bool operator <(IntReference left, IntReference right) { return left.Value < right.Value; }
    public static bool operator >(IntReference left, IntReference right) { return left.Value > right.Value; }
    public static bool operator <=(IntReference left, IntReference right) { return left.Value <= right.Value; }
    public static bool operator >=(IntReference left, IntReference right) { return left.Value >= right.Value; }

    public static float operator +(IntReference left, IntReference right) { return left.Value + right.Value; }
    public static float operator -(IntReference left, IntReference right) { return left.Value - right.Value; }
    public static float operator *(IntReference left, IntReference right) { return left.Value * right.Value; }
    public static float operator /(IntReference left, IntReference right) { return left.Value / right.Value; }

    public static IntReference operator --(IntReference instance) { instance.Value--; return instance; }
    public static IntReference operator ++(IntReference instance) { instance.Value++; return instance; }

    public override bool Equals(object obj) { return base.Equals(obj); }
    public override int GetHashCode() { return base.GetHashCode(); }
}
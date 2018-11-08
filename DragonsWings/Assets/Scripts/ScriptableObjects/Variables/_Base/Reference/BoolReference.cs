using UnityEngine;

[System.Serializable]
public class BoolReference : BaseReference<BoolVariable, BoolMap, bool>
{
    public static bool operator ==(BoolReference left, BoolReference right) { return left == right; }
    public static bool operator !=(BoolReference left, BoolReference right) { return left != !right; }
    public static bool operator <(BoolReference left, BoolReference right) { return !left && right; }
    public static bool operator >(BoolReference left, BoolReference right) { return left && !right; }
    public static bool operator <=(BoolReference left, BoolReference right) { return (left == right) || (left < right); }
    public static bool operator >=(BoolReference left, BoolReference right) { return (left == right) || (left > right); }

    public override bool Equals(object obj) { return base.Equals(obj); }
    public override int GetHashCode() { return base.GetHashCode(); }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Maps/Transform List Map")]
public class BaseListMap<T> : BaseMap<System.Collections.Generic.List<T>>
{
    public int Length(Transform identifier)
    { return Get(identifier).Count; }
}
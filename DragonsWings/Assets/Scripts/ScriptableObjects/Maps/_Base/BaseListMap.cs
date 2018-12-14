using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Maps/List Map")]
public class BaseListMap<T> : BaseMap<System.Collections.Generic.List<T>>
{
    public int Length(GameObject identifier)
    { return Get(identifier).Count; }
}
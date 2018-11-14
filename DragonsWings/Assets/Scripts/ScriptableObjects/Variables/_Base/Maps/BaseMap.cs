using UnityEngine;

public abstract class BaseMap<T> : ScriptableObject
{
    protected System.Collections.Generic.Dictionary<GameObject, T> Items = new System.Collections.Generic.Dictionary<GameObject, T>();

    public void Add(GameObject identifier, T item)
    { Items.Add(identifier, item); }

    public T Get(GameObject identifier)
    {
        T value;
        Items.TryGetValue(identifier, out value);
        return value;
    }

    public void Set(GameObject identifier, T item)
    { Items[identifier] = item; }

    public void Remove(GameObject identifier)
    { Items.Remove(identifier); }

    public void Clear()
    { Items = new System.Collections.Generic.Dictionary<GameObject, T>(); }
}
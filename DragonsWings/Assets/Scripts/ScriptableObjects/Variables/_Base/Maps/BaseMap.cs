using UnityEngine;

public abstract class BaseMap<T> : ScriptableObject
{
    protected System.Collections.Generic.Dictionary<Transform, T> Items = new System.Collections.Generic.Dictionary<Transform, T>();

    public void Add(Transform identifier, T item)
    { Items.Add(identifier, item); }

    public T Get(Transform identifier)
    { return Items[identifier]; }

    public void Set(Transform identifier, T item)
    { Items[identifier] = item; }

    public void Remove(Transform identifier)
    { Items.Remove(identifier); }

    public void Clear()
    { Items = new System.Collections.Generic.Dictionary<Transform, T>(); }
}
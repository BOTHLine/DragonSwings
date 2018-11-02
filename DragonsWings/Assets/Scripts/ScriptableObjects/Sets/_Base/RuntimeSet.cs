using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    public System.Collections.Generic.List<T> Items = new System.Collections.Generic.List<T>();

    public void Add(T item)
    { if (!Items.Contains(item)) Items.Add(item); }

    public void Remove(T item)
    { if (Items.Contains(item)) Items.Remove(item); }

    public void Clear()
    { Items = new System.Collections.Generic.List<T>(); }
}
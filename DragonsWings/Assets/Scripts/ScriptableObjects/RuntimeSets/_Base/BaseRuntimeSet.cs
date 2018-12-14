using UnityEngine;

public class BaseRuntimeSet<T> : ScriptableObject
{
    public System.Collections.Generic.List<T> _Items = new System.Collections.Generic.List<T>();
    public T[] Items
    {
        get
        {
            T[] itemArray = new T[_Items.Count];
            _Items.CopyTo(itemArray);
            return itemArray;
        }
    }

    public int Count { get { return _Items.Count; } }

    public System.Action<T> _OnItemAdded = delegate { };
    public System.Action<T> _OnItemRemoved = delegate { };

    public void Add(T item)
    {
        if (!_Items.Contains(item))
        {
            _Items.Add(item);
            _OnItemAdded(item);
        }
    }

    public bool Contains(T item)
    { return _Items.Contains(item); }

    public void Remove(T item)
    {
        if (_Items.Contains(item))
        {
            _Items.Remove(item);
            _OnItemRemoved.Invoke(item);
        }
    }

    public void Clear()
    {
        _Items.Clear();
    }
}
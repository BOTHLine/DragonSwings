using UnityEngine;

[System.Serializable]
public abstract class BaseMap<T> : ScriptableObject, ISerializationCallbackReceiver
{
    [HideInInspector] [SerializeField] public GameObject[] Keys;
    [SerializeField] public string[] KeyNames;
    [SerializeField] public T[] Values;

    public System.Collections.Generic.Dictionary<GameObject, T> Items = new System.Collections.Generic.Dictionary<GameObject, T>();

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

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        Keys = new GameObject[Items.Keys.Count];
        Items.Keys.CopyTo(Keys, 0);

        KeyNames = new string[Keys.Length];
        for (int i = 0; i < Keys.Length; i++)
        {
            KeyNames[i] = Keys[i].GetFullName();
        }

        Values = new T[Items.Values.Count];
        Items.Values.CopyTo(Values, 0);
    }
}
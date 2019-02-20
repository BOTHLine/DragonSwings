using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseMap<TDatatype> : ScriptableObject, ISerializationCallbackReceiver
{
    [HideInInspector] [SerializeField] public GameObject[] Keys;
    [SerializeField] public string[] KeyNames;
    [SerializeField] public TDatatype[] Values;

    protected Dictionary<GameObject, TDatatype> Items = new Dictionary<GameObject, TDatatype>();
    private Dictionary<GameObject, System.Action<TDatatype>> OnValueChanges = new Dictionary<GameObject, System.Action<TDatatype>>();

    public void Add(GameObject identifier, TDatatype item)
    { Items.Add(identifier, item); }

    public TDatatype Get(GameObject identifier)
    {
        TDatatype value;
        Items.TryGetValue(identifier, out value);
        return value;
    }

    public void Set(GameObject identifier, TDatatype item)
    {
        Items[identifier] = item;
        GetActions(identifier)(item);
    }

    public void Remove(GameObject identifier)
    { Items.Remove(identifier); }

    public void Clear()
    { Items = new Dictionary<GameObject, TDatatype>(); }

    public void Subscribe(GameObject identifier, System.Action<TDatatype> action)
    {
        System.Action<TDatatype> actions = GetActions(identifier);
        actions += action;
        OnValueChanges[identifier] = actions;
    }

    public void Unsubscribe(GameObject identifier, System.Action<TDatatype> action)
    {
        System.Action<TDatatype> actions = GetActions(identifier);
        actions -= action;
        OnValueChanges[identifier] = actions;
    }

    private System.Action<TDatatype> GetActions(GameObject identifier)
    {
        System.Action<TDatatype> actions;
        OnValueChanges.TryGetValue(identifier, out actions);
        if (actions == null)
        {
            actions = delegate { };
            OnValueChanges[identifier] = actions;
        }
        return actions;
    }

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

        Values = new TDatatype[Items.Values.Count];
        Items.Values.CopyTo(Values, 0);
    }
}
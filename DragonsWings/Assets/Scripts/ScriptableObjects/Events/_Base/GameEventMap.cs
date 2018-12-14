using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game Event Map")]
public class GameEventMap : ScriptableObject
{
    private Dictionary<GameObject, List<UnityEvent>> _UnityEventListener = new Dictionary<GameObject, List<UnityEvent>>();

    public void Raise(GameObject key)
    {
        Debug.Log("EventMap: " + name + " raised for: " + key.name);

        List<UnityEvent> list;
        _UnityEventListener.TryGetValue(key, out list);

        if (list == null) { return; }

        for (int i = list.Count - 1; i >= 0; i--)
        { list[i].Invoke(); }
    }

    public void RaiseAll()
    {
        Debug.Log("EventMap: " + name + " raised for All!");

        List<UnityEvent>[] lists = new List<UnityEvent>[_UnityEventListener.Values.Count];
        _UnityEventListener.Values.CopyTo(lists, 0);
        foreach (List<UnityEvent> list in lists)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            { list[i].Invoke(); }
        }
    }

    public void RegisterListener(GameObject key, UnityEvent listener)
    {
        List<UnityEvent> list;
        _UnityEventListener.TryGetValue(key, out list);
        if (list == null)
        {
            list = new List<UnityEvent>();
            list.Add(listener);
        }
        else
        {
            if (!list.Contains(listener))
            { list.Add(listener); }
        }
        _UnityEventListener[key] = list;
    }

    public void UnregisterListener(GameObject key, UnityEvent listener)
    {
        List<UnityEvent> list;
        _UnityEventListener.TryGetValue(key, out list);
        if (list != null && list.Contains(listener))
        {
            list.Remove(listener);
            _UnityEventListener[key] = list;
        }
    }

    public void OnAfterDeserialize() { }
    public void OnBeforeSerialize() { }
}
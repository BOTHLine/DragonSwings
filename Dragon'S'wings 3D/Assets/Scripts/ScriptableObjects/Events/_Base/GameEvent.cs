using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<UnityEvent> _UnityEventListener = new List<UnityEvent>();
    private event System.Action _ActionListener = delegate { };

    public void Raise()
    {
        Debug.Log("Event: " + name + " raised!");
        for (int i = _UnityEventListener.Count - 1; i >= 0; i--)
        { _UnityEventListener[i].Invoke(); }
        _ActionListener.Invoke();
    }

    public void RegisterListener(UnityEvent listener)
    { if (!_UnityEventListener.Contains(listener)) { _UnityEventListener.Add(listener); } }

    public void UnregisterListener(UnityEvent listener)
    { if (_UnityEventListener.Contains(listener)) { _UnityEventListener.Remove(listener); } }

    public void RegisterListener(System.Action listener)
    { _ActionListener += listener; }

    public void UnregisterListener(System.Action listener)
    { _ActionListener -= listener; }

    public void OnAfterDeserialize() { }
    public void OnBeforeSerialize() { }
}
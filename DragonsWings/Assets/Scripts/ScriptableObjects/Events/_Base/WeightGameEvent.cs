﻿using UnityEngine;

[CreateAssetMenu(menuName = "Events/Weight Game Event")]
public class WeightGameEvent : ScriptableObject
{
    private System.Collections.Generic.List<UnityEngine.Events.UnityEvent<Weight>> listener = new System.Collections.Generic.List<UnityEngine.Events.UnityEvent<Weight>>();
    //   private System.Collections.Generic.List<GameEventListener> Listeners = new System.Collections.Generic.List<GameEventListener>();

    public void Raise(Weight weight)
    {
        for (int i = listener.Count - 1; i >= 0; i--)
        {
            listener[i].Invoke(weight);
        }
    }

    public void RegisterListener(UnityEngine.Events.UnityEvent<Weight> _listener)
    { if (!listener.Contains(_listener)) listener.Add(_listener); }

    public void UnregisterListener(UnityEngine.Events.UnityEvent<Weight> _listener)
    { if (listener.Contains(_listener)) listener.Remove(_listener); }

    public void OnAfterDeserialize() { }
    public void OnBeforeSerialize() { }
}
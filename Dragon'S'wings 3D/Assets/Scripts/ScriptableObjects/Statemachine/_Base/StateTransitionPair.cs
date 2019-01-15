using UnityEngine;

[System.Serializable]
public struct StateTransitionPair
{
    public State fromState;
    public Transition[] transitionsTo;
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Statemachine")]
public class Statemachine : ScriptableObject
{
    public State currentState;
    public State[] states;
    public System.Collections.Generic.Dictionary<State, Transition[]> transitions;
}
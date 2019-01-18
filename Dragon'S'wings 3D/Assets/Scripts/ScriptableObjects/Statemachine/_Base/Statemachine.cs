using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Statemachine")]
public class Statemachine : ScriptableObject
{
    public State _InitialState;

    public Transition[] _TransitionsFromAnyState;
    public StateTransitionPair[] _States;

    public State Initialize(StateController controller)
    {
        _InitialState.EnterState(controller);
        return _InitialState;
    }

    public void UpdateStatemachine(StateController controller, State state)
    {
        state.DoActions(controller);
        if (CheckTransitionsFromAnyState(controller)) return;
        CheckTransitionsFromCurrentState(controller);
    }

    private bool CheckTransitionsFromAnyState(StateController controller)
    {
        foreach (Transition transition in _TransitionsFromAnyState)
        {
            if (controller.TransitionToState(transition.GetTargetState(controller)))
                return true;
        }
        return false;
    }

    private bool CheckTransitionsFromCurrentState(StateController controller)
    {
        foreach (StateTransitionPair state in _States)
        {
            if (state.fromState == controller._CurrentState)
            {
                foreach (Transition transition in state.transitionsTo)
                {
                    if (controller.TransitionToState(transition.GetTargetState(controller)))
                        return true;
                }
            }
        }
        return false;
    }

    public void EnterState(StateController controller, State currentState, State nextState)
    {
        currentState.ExitState(controller);
        foreach (StateTransitionPair state in _States)
        {
            if (state.fromState == currentState)
            {
                foreach (Transition transition in state.transitionsTo)
                { transition.ExitState(controller); }
                return;
            }
        }

        nextState.EnterState(controller);
        foreach (StateTransitionPair state in _States)
        {
            if (state.fromState == nextState)
            {
                foreach (Transition transition in state.transitionsTo)
                { transition.EnterState(controller); }
                return;
            }
        }
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/State")]
public class State : ScriptableObject
{
    public Layer _Layer;

    public Behaviour[] _Behaviours;

    public Transition[] _Transitions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        foreach (Behaviour behaviour in _Behaviours)
        { behaviour.Act(controller); }
    }

    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < _Transitions.Length; i++)
        {
            if (controller.TransitionToState(_Transitions[i].GetTargetState(controller)))
                break;
        }
    }

    public void EnterState(StateController controller)
    {
        Debug.Log("Enter State: " + name);

        PushBox pushBox = controller.GetComponentInChildren<PushBox>();
        if (pushBox != null) { pushBox.gameObject.layer = (int)_Layer; };

        foreach (Transition transition in _Transitions)
        { transition.EnterState(controller); }

        foreach (Behaviour behaviour in _Behaviours)
        { behaviour.EnterState(controller); }
    }

    public void ExitState(StateController controller)
    {
        foreach (Behaviour behaviour in _Behaviours)
        { behaviour.ExitState(controller); }

        foreach (Transition transition in _Transitions)
        { transition.ExitState(controller); }
    }
}
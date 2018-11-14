using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/State")]
public class State : ScriptableObject
{
    public Layer layer;

    public Action[] actions;
    public Transition[] transitions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool shouldTransition = true;
            for (int j = 0; j < transitions[i].decisions.Length; j++)
            {
                if (!transitions[i].decisions[j].Decide(controller)) { shouldTransition = false; }
            }
            State transitionState = shouldTransition ? transitions[i].trueState : transitions[i].falseState;
            if (controller.TransitionToState(transitionState))
                break;
        }
    }

    public void EnterState(StateController controller)
    {
        controller.gameObject.layer = (int)layer;

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].EnterState(controller);
        }

        for (int i = 0; i < transitions.Length; i++)
        {
            for (int j = 0; j < transitions[i].decisions.Length; j++)
            {
                transitions[i].decisions[j].EnterState(controller);
            }
        }
    }

    public void ExitState(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].ExitState(controller);
        }

        for (int i = 0; i < transitions.Length; i++)
        {
            for (int j = 0; j < transitions[i].decisions.Length; j++)
            {
                transitions[i].decisions[j].ExitState(controller);
            }
        }
    }
}
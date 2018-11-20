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
            if (controller.TransitionToState(transitions[i].GetTargetState(controller)))
                break;
        }
    }

    public void EnterState(StateController controller)
    {
        //   Debug.Log("Enter State: " + name);

        controller.gameObject.layer = (int)layer;

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].EnterState(controller);
        }

        for (int i = 0; i < transitions.Length; i++)
        {
            transitions[i].EnterState(controller);
        }
    }

    public void ExitState(StateController controller)
    {
        //   Debug.Log("Exit State: " + name);

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].ExitState(controller);
        }

        for (int i = 0; i < transitions.Length; i++)
        {
            transitions[i].ExitState(controller);
        }
    }
}
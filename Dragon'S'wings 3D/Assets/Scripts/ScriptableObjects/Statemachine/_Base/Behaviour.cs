using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Behaviour")]
public class Behaviour : ScriptableObject
{
    public Action[] _EnterStateActions;
    public Action[] _ActActions;
    public Action[] _ExitStateActions;

    public void EnterState(StateController controller)
    {
        foreach (Action action in _EnterStateActions)
        { action.Act(controller); }
    }

    public void Act(StateController controller)
    {
        foreach (Action action in _ActActions)
        { action.Act(controller); }
    }

    public void ExitState(StateController controller)
    {
        foreach (Action action in _ExitStateActions)
        { action.Act(controller); }
    }
}
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public abstract void Act(StateController controller);

    public abstract void EnterState(StateController controller);
    public abstract void ExitState(StateController controller);
}
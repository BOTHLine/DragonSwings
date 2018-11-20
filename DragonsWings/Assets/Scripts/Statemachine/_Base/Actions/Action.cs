using UnityEngine;

public abstract class Action : ScriptableObject
{
    public abstract void Act(StateController controller);

    public virtual void EnterState(StateController controller) { }
    public virtual void ExitState(StateController controller) { }
}
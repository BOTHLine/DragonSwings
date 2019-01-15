using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(StateController controller);

    public virtual void EnterState(StateController controller) { }
    public virtual void ExitState(StateController controller) { }
}
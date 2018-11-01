using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(StateController controller);

    public abstract void EnterState(StateController controller);
    public abstract void ExitState(StateController controller);
}
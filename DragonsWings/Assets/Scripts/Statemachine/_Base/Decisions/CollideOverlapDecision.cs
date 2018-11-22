using UnityEngine;

public class CollideOverlapDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return false;
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }
}
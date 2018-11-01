using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Fall Action")]
public class FallAction : Action
{
    public FloatReference fallTime;
    private float currentFallTime;

    public override void Act(StateController controller)
    {
        currentFallTime += Time.deltaTime;
        if (currentFallTime >= fallTime)
            ;
    }

    public override void EnterState(StateController controller) { currentFallTime = 0.0f; }
    public override void ExitState(StateController controller) { }
}
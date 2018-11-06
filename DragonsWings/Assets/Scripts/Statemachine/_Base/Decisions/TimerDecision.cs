using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Timer Decision")]
public class TimerDecision : Decision
{
    public FloatReference time;
    private float currentTime;

    public override bool Decide(StateController controller)
    {
        currentTime += Time.deltaTime;
        return currentTime >= time;
    }

    public override void EnterState(StateController controller)
    { currentTime = 0.0f; }

    public override void ExitState(StateController controller) { }
}
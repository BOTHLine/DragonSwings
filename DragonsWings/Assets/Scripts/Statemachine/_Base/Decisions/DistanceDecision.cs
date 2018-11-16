using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Distance Decision")]
public class DistanceDecision : Decision
{
    public Vector2Reference position;
    public Vector2Reference targetPosition;
    public FloatReference maxDistance;
    public FloatReference threshold;

    public override bool Decide(StateController controller)
    {
        float squaredMaxDistance = Mathf.Pow(maxDistance.Get(controller.gameObject), 2);
        float squaredThreshold = Mathf.Pow(threshold.Get(controller.gameObject), 2);
        float squaredDistance = (position.Get(controller.gameObject) - targetPosition.Get(controller.gameObject)).sqrMagnitude;

        return squaredDistance <= squaredMaxDistance + squaredThreshold;
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }
}
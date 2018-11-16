using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/RunAway Action")]
public class RunAwayAction : Action
{
    public Vector2Reference targetPosition;
    public FloatReference maxDistance;

    public Vector2Reference moveDirection;

    public override void Act(StateController controller)
    {
        Vector2 distanceVector = targetPosition.Get(controller.gameObject) - (Vector2)controller.transform.position;

        if (distanceVector.sqrMagnitude <= maxDistance.Get(controller.gameObject) * maxDistance.Get(controller.gameObject))
        { moveDirection.Set(-distanceVector.normalized, controller.gameObject); }
        else
        { moveDirection.Set(Vector2.zero); }
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Chase Action")]
public class ChaseAction : Action
{
    public Vector2Reference targetPosition;
    public Vector2Reference moveDirection;

    public override void Act(StateController controller)
    { moveDirection.Value = (targetPosition - (Vector2)controller.transform.position).normalized; }

    public override void EnterState(StateController controller)
    { if (moveDirection.MapIdentifier == null) moveDirection.MapIdentifier = controller.gameObject; }

    public override void ExitState(StateController controller) { }
}
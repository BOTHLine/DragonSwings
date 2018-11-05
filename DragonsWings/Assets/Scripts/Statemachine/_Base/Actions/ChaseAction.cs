using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Chase Action")]
public class ChaseAction : Action
{
    public Vector2Reference targetPosition;
    public Vector2Reference moveDirection;

    public override void Act(StateController controller)
    {
        moveDirection.Variable.Value = (targetPosition - (Vector2)controller.transform.position).normalized;
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }
}
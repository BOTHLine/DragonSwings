using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Move Action")]
public class MoveAction : Action
{
    public Vector2Reference moveDirection;
    public FloatReference moveSpeed;

    public Vector2Reference lastSavePosition;

    // TODO Vector2Reference moveDirection

    public override void Act(StateController controller)
    {
        //   controller.rigidbody2D.AddForce(moveDirection.Value * moveSpeed);
        controller.rigidbody2D.velocity = moveDirection.Value * moveSpeed;
    }

    public override void EnterState(StateController controller) { controller.canDash = true; }
    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
        lastSavePosition.Variable.Value = controller.transform.position;
    }
}
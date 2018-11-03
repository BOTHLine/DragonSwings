using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Move Action")]
public class MoveAction : Action
{
    public Vector2Reference moveDirection;
    public FloatReference moveSpeed;

    public Vector2Reference lastSavePosition;

    public override void Act(StateController controller)
    {
        controller.rigidbody2D.velocity = moveDirection.Value * moveSpeed;

        controller.animator.SetBool("IsMoving", controller.rigidbody2D.velocity.x != 0 || controller.rigidbody2D.velocity.y != 0);
        controller.animator.SetBool("IsMovingHorizontally", Mathf.Abs(controller.rigidbody2D.velocity.x) > Mathf.Abs(controller.rigidbody2D.velocity.y));
        controller.animator.SetFloat("VelocityX", controller.rigidbody2D.velocity.x);
        controller.animator.SetFloat("VelocityY", controller.rigidbody2D.velocity.y);
    }

    public override void EnterState(StateController controller) { controller.canDash = true; }
    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
        lastSavePosition.Variable.Value = controller.transform.position;
    }
}
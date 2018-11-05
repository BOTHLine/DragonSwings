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
<<<<<<< HEAD
        controller.animator.SetBool("IsMovingHorizontal", Mathf.Abs(moveDirection.Value.x) > Mathf.Abs(moveDirection.Value.y));
        controller.animator.SetFloat("VelocityX", moveDirection.Value.x);
        controller.animator.SetFloat("VelocityY", moveDirection.Value.y);
    }

    public override void EnterState(StateController controller)
    {
        controller.animator.SetBool("IsMoving", true);
=======

        controller.animator.SetBool("IsMoving", controller.rigidbody2D.velocity.x != 0 || controller.rigidbody2D.velocity.y != 0);
        controller.animator.SetFloat("VelocityX", controller.rigidbody2D.velocity.x);
        controller.animator.SetFloat("VelocityY", controller.rigidbody2D.velocity.y);

        if (controller.rigidbody2D.velocity.sqrMagnitude != 0.0f)
        {
            controller.animator.SetFloat("LastVelocityX", controller.rigidbody2D.velocity.x);
            controller.animator.SetFloat("LastVelocityY", controller.rigidbody2D.velocity.y);
        }
>>>>>>> c828cadd280b1dbd43860dbbea8e2770135d25a1
    }

    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
        lastSavePosition.Variable.Value = controller.transform.position;
        controller.animator.SetBool("IsMoving", false);
    }
}
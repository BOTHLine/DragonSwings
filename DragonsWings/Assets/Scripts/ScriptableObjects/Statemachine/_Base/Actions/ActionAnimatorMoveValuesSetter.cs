using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Animator Move Value Setter")]
public class ActionAnimatorMoveValuesSetter : Action
{
    public override void Act(StateController controller)
    {
        controller.animator.SetBool("IsMoving", controller.rigidbody2D.velocity.x != 0 || controller.rigidbody2D.velocity.y != 0);

        controller.animator.SetFloat("VelocityX", controller.rigidbody2D.velocity.x);
        controller.animator.SetFloat("VelocityY", controller.rigidbody2D.velocity.y);

        if (controller.rigidbody2D.velocity.sqrMagnitude != 0.0f)
        {
            controller.animator.SetFloat("LastVelocityX", controller.rigidbody2D.velocity.x);
            controller.animator.SetFloat("LastVelocityY", controller.rigidbody2D.velocity.y);
        }
    }
}

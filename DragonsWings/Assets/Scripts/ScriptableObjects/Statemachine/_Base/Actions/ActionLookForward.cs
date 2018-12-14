using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/LookForward")]
public class ActionLookForward : Action
{
    public override void Act(StateController controller)
    {
        controller.animator.SetFloat("VelocityX", controller.rigidbody2D.velocity.x);
        controller.animator.SetFloat("VelocityY", controller.rigidbody2D.velocity.y);

        controller.animator.SetFloat("LastVelocityX", controller.rigidbody2D.velocity.x);
        controller.animator.SetFloat("LastVelocityY", controller.rigidbody2D.velocity.y);
    }
}
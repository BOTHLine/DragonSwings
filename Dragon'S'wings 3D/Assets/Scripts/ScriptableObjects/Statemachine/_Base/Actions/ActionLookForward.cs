using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/LookForward")]
public class ActionLookForward : Action
{
    public override void Act(StateController controller)
    {
        controller._Animator.SetFloat("VelocityX", controller._Rigidbody.velocity.x);
        controller._Animator.SetFloat("VelocityZ", controller._Rigidbody.velocity.z);

        controller._Animator.SetFloat("LastVelocityX", controller._Rigidbody.velocity.x);
        controller._Animator.SetFloat("LastVelocityZ", controller._Rigidbody.velocity.z);
    }
}
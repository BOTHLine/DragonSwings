using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Look At Target")]
public class ActionLookAtTarget : Action
{
    public Vector3Reference targetPosition;

    public override void Act(StateController controller)
    {
        Vector3 directionVector = targetPosition.Get(controller.gameObject) - controller.transform.position;

        if (controller._Animator.GetBool("IsMoving"))
        {
            controller._Animator.SetFloat("VelocityX", directionVector.x);
            controller._Animator.SetFloat("VelocityZ", directionVector.z);
        }
        else
        {
            controller._Animator.SetFloat("LastVelocityX", directionVector.x);
            controller._Animator.SetFloat("LastVelocityZ", directionVector.z);
        }
    }
}
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
        controller.animator.SetBool("IsMovingHorizontal", Mathf.Abs(moveDirection.Value.x) > Mathf.Abs(moveDirection.Value.y));
        controller.animator.SetFloat("VelocityX", moveDirection.Value.x);
        controller.animator.SetFloat("VelocityY", moveDirection.Value.y);
    }

    public override void EnterState(StateController controller)
    {
        controller.animator.SetBool("IsMoving", true);
    }

    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
        lastSavePosition.Variable.Value = controller.transform.position;
        controller.animator.SetBool("IsMoving", false);
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Move Action")]
public class MoveAction : Action
{
    public Vector2Map _MoveDirectionMap;
    // public Vector2Reference moveDirection;
    public FloatReference moveSpeed;

    // public Vector2Reference lastSavePosition;
    public Vector2Map _LastSavePositionMap;

    public override void Act(StateController controller)
    {
        controller.rigidbody2D.velocity = _MoveDirectionMap.Get(controller.gameObject) * moveSpeed.Get(controller.gameObject);
        //    controller.rigidbody2D.velocity = moveDirection.Get(controller.gameObject) * moveSpeed.Get(controller.gameObject);

        controller.animator.SetBool("IsMoving", controller.rigidbody2D.velocity.x != 0 || controller.rigidbody2D.velocity.y != 0);

        controller.animator.SetFloat("VelocityX", controller.rigidbody2D.velocity.x);
        controller.animator.SetFloat("VelocityY", controller.rigidbody2D.velocity.y);

        if (controller.rigidbody2D.velocity.sqrMagnitude != 0.0f)
        {
            controller.animator.SetFloat("LastVelocityX", controller.rigidbody2D.velocity.x);
            controller.animator.SetFloat("LastVelocityY", controller.rigidbody2D.velocity.y);
        }
    }

    public override void EnterState(StateController controller) { }

    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
        _LastSavePositionMap.Set(controller.gameObject, controller.transform.position);
        controller.animator.SetBool("IsMoving", false);
    }
}
﻿using UnityEngine;

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

        controller.animator.SetFloat("VelocityX", controller.rigidbody2D.velocity.x);
        controller.animator.SetFloat("VelocityY", controller.rigidbody2D.velocity.y);

        if (controller.rigidbody2D.velocity.sqrMagnitude != 0.0f)
        {
            controller.animator.SetFloat("LastVelocityX", controller.rigidbody2D.velocity.x);
            controller.animator.SetFloat("LastVelocityY", controller.rigidbody2D.velocity.y);
        }
    }

    public override void EnterState(StateController controller)
    {
        moveDirection.SetEmptyMapIdentifier(controller.gameObject);
        moveSpeed.SetEmptyMapIdentifier(controller.gameObject);
        lastSavePosition.SetEmptyMapIdentifier(controller.gameObject);
    }

    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
        lastSavePosition.Value = controller.transform.position;
        controller.animator.SetBool("IsMoving", false);
    }
}
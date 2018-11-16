﻿using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/LookAtTarget Action")]
public class LookAtTargetAction : Action
{
    public Vector2Reference targetPosition;

    public override void Act(StateController controller)
    {
        Vector2 directionVector = targetPosition.Get(controller.gameObject) - (Vector2)controller.transform.position;

        if (controller.rigidbody2D.velocity.sqrMagnitude != 0.0f)
        {
            controller.animator.SetFloat("LastVelocityX", directionVector.x);
            controller.animator.SetFloat("LastVelocityY", directionVector.y);
        }
        else
        {
            controller.animator.SetFloat("VelocityX", directionVector.x);
            controller.animator.SetFloat("VelocityY", directionVector.y);
        }
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }
}
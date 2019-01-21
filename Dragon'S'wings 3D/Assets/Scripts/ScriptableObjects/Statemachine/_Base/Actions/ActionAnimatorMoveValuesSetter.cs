﻿using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Animator Move Value Setter")]
public class ActionAnimatorMoveValuesSetter : Action
{
    public override void Act(StateController controller)
    {
        controller._Animator.SetBool("IsMoving", controller._Rigidbody.velocity.x != 0 || controller._Rigidbody.velocity.y != 0);

        controller._Animator.SetFloat("VelocityX", controller._Rigidbody.velocity.x);
        controller._Animator.SetFloat("VelocityY", controller._Rigidbody.velocity.y);

        if (controller._Rigidbody.velocity.sqrMagnitude != 0.0f)
        {
            controller._Animator.SetFloat("LastVelocityX", controller._Rigidbody.velocity.x);
            controller._Animator.SetFloat("LastVelocityY", controller._Rigidbody.velocity.y);
        }
    }
}
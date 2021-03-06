﻿using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Respawn")]
public class ActionRespawn : Action
{
    //public Vector2Reference lastSavePosition;
    public Vector2Map _LastSavePositionMap;

    public FloatReference health;

    public GameEvent OnPlayerRespawn;

    public override void Act(StateController controller)
    { OnPlayerRespawn.Raise(); }

    /*
    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
        //   controller.rigidbody2D.MovePosition(lastSavePosition);
        controller.transform.position = _LastSavePositionMap.Get(controller.gameObject);
        controller.spriteRenderer.enabled = true;
    }
    */
}
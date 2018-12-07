﻿using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/ReduceAttackCooldown Action")]
public class ReduceAttackCooldownAction : Action
{
    public FloatMap _CurrentAttackCoolDownMap;
    //   public FloatReference _CurrentAttackCooldown;
    public FloatReference _ReduceFactor;

    public override void Act(StateController controller)
    //{ _CurrentAttackCooldown.ChangeValue(-(Time.deltaTime * _ReduceFactor.Get(controller.gameObject)), controller.gameObject); }
    { _CurrentAttackCoolDownMap.ChangeValue(controller.gameObject, -Time.deltaTime * _ReduceFactor.Value); }
}
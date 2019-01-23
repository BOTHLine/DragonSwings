using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Float Value Time Changer")]
public class ActionFloatValueTimeChanger : Action
{
    public FloatMap _CurrentValueMap;
    //   public FloatReference _CurrentAttackCooldown;
    public FloatReference _ChangeFactor;

    public override void Act(StateController controller)
    //{ _CurrentAttackCooldown.ChangeValue(-(Time.deltaTime * _ReduceFactor.Get(controller.gameObject)), controller.gameObject); }
    { _CurrentValueMap.ChangeValue(controller.gameObject, Time.deltaTime * _ChangeFactor.Get(controller.gameObject)); }
}
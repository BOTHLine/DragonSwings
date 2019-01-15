using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Animator Float Setter")]
public class ActionAnimatorFloatSetter : Action
{
    public string _Name;
    public FloatReference _Value;

    public override void Act(StateController controller)
    { controller.animator.SetFloat(_Name, _Value.Get(controller.gameObject)); }
}
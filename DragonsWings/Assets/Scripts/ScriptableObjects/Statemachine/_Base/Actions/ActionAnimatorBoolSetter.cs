using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Animator Bool Setter")]
public class ActionAnimatorBoolSetter : Action
{
    public string _Name;
    public BoolReference _Value;

    public override void Act(StateController controller)
    { controller.animator.SetBool(_Name, _Value.Get(controller.gameObject)); }
}

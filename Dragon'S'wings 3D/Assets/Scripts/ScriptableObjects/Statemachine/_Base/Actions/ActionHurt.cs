using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Hurt")]
public class ActionHurt : Action
{
    public FloatReference _Damage;

    public override void Act(StateController controller)
    { controller.GetComponentInChildren<HurtBox>()?.Hurt(_Damage.Value); }
}
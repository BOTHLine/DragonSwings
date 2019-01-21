using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/HurtboxList RuntimeSet Empty")]
public class ActionRuntimeSetHurtboxEmpty : Action
{
    public HurtBoxListRuntimeSet _RuntimeSet;

    public override void Act(StateController controller)
    { _RuntimeSet.Clear(); }
}
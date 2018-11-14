using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Reposition Action")]
public class RepositionAction : Action
{
    public Vector2Reference targetPosition;
    private Vector2 Startposition;

    public override void Act(StateController controller)
    {

    }

    public override void EnterState(StateController controller)
    { targetPosition.SetEmptyMapIdentifier(controller.gameObject); }

    public override void ExitState(StateController controller) { }
}
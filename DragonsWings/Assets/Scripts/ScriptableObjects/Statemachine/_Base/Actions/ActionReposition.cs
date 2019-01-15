using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Reposition")]
public class ActionResposition : Action
{
    public Vector2Reference targetPosition;
    private Vector2 Startposition;

    public override void Act(StateController controller)
    {
        targetPosition.MapIdentifier = controller.gameObject;
    }
}
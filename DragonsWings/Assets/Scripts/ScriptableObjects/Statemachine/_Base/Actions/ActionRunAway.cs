using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/RunAway")]
public class ActionRunAway : Action
{
    public Vector2Reference targetPosition;
    public FloatReference maxDistance;

    // public Vector2Reference moveDirection;
    public Vector2Map _MoveDirectionMap;

    public override void Act(StateController controller)
    {
        Vector2 distanceVector = targetPosition.Get(controller.gameObject) - (Vector2)controller.transform.position;

        if (distanceVector.sqrMagnitude <= maxDistance.Get(controller.gameObject) * maxDistance.Get(controller.gameObject))
        { _MoveDirectionMap.Set(controller.gameObject, -distanceVector.normalized); }
        else
        { _MoveDirectionMap.Set(controller.gameObject, Vector2.zero); }
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Chase")]
public class ActionChase : Action
{
    public Vector2Reference targetPosition;
    public FloatReference minDistance;

    public Vector2Map moveDirectionMap;
    // public Vector2Reference moveDirection;

    public override void Act(StateController controller)
    {
        Vector2 distanceVector = targetPosition.Get(controller.gameObject) - (Vector2)controller.transform.position;

        if (distanceVector.sqrMagnitude >= minDistance.Get(controller.gameObject) * minDistance.Get(controller.gameObject))
        //    { moveDirection.Set(distanceVector.normalized, controller.gameObject); }
        { moveDirectionMap.Set(controller.gameObject, distanceVector.normalized); }
        else
        //    { moveDirection.Set(Vector2.zero, controller.gameObject); }
        { moveDirectionMap.Set(controller.gameObject, Vector2.zero); }
    }
}
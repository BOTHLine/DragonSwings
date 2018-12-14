using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Find Next Waypoint")]
public class ActionFindNextWaypoint : Action
{
    public TransformListMap waypointSet;
    private int currentWaypoint = 0;

    public Vector2Reference moveDirection;
    public FloatReference distanceThreshold;

    public override void Act(StateController controller)
    {
        if (HasReachedWaypoint(controller))
            currentWaypoint = (currentWaypoint + 1) % waypointSet.Length(controller.gameObject);

        moveDirection.Set(((Vector2)(waypointSet.Get(controller.gameObject)[currentWaypoint].position - controller.transform.position)).normalized, controller.gameObject);
    }

    private bool HasReachedWaypoint(StateController controller)
    {
        float squaredDistance = ((Vector2)(waypointSet.Get(controller.gameObject)[currentWaypoint].position - controller.transform.position)).sqrMagnitude;
        return squaredDistance <= distanceThreshold.Get(controller.gameObject) * distanceThreshold.Get(controller.gameObject);
    }
}
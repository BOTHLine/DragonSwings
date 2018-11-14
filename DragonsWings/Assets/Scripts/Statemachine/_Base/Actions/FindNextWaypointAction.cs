using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/FindNextWaypoint Action")]
public class FindNextWaypointAction : Action
{
    public TransformListMap waypointSet;
    private int currentWaypoint = 0;

    public Vector2Reference moveDirection;
    public FloatReference distanceThreshold;

    public override void Act(StateController controller)
    {
        if (ReachedWaypoint(controller))
            currentWaypoint = (currentWaypoint + 1) % waypointSet.Length(controller.gameObject);

        moveDirection.Variable.Value = ((Vector2)(waypointSet.Get(controller.gameObject)[currentWaypoint].position - controller.transform.position)).normalized;
    }

    public override void EnterState(StateController controller)
    {
        moveDirection.SetEmptyMapIdentifier(controller.gameObject);
        distanceThreshold.SetEmptyMapIdentifier(controller.gameObject);
    }

    public override void ExitState(StateController controller) { }

    private bool ReachedWaypoint(StateController controller)
    {
        float squaredDistance = ((Vector2)(waypointSet.Get(controller.gameObject)[currentWaypoint].position - controller.transform.position)).sqrMagnitude;
        return squaredDistance <= distanceThreshold * distanceThreshold;
    }
}
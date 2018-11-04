using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/FindNextWaypoint Action")]
public class FindNextWaypointAction : Action
{
    public WaypointSet waypointSet;
    private int currentWaypoint = 0;

    public Vector2Reference moveDirection;
    public FloatReference distanceThreshold;

    public override void Act(StateController controller)
    {
        if (ReachedWaypoint(controller))
            currentWaypoint = (currentWaypoint + 1) % waypointSet.Items[controller.transform].Count;

        moveDirection.Variable.Value = ((Vector2)(waypointSet.Items[controller.transform][currentWaypoint].position - controller.transform.position)).normalized;
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }

    private bool ReachedWaypoint(StateController controller)
    {
        float squaredDistance = ((Vector2)(waypointSet.Items[controller.transform][currentWaypoint].position - controller.transform.position)).sqrMagnitude;
        return squaredDistance <= distanceThreshold * distanceThreshold;
    }
}
using UnityEngine;

public class WaypointSetFiller : MonoBehaviour
{
    public TransformListMap waypointSet;
    public GameObject enemy;

    private void Awake()
    {
        waypointSet.Clear();
        System.Collections.Generic.List<Transform> waypoints = new System.Collections.Generic.List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(transform.GetChild(i));
        }
        waypointSet.Add(enemy, waypoints);
    }
}
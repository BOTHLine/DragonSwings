using UnityEngine;

public class WaypointSetFiller : MonoBehaviour
{
    public WaypointSet waypointSet;

    private void Awake()
    {
        waypointSet.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            waypointSet.Add(transform.GetChild(i));
        }
    }
}
using UnityEngine;

public class ShowAimPosition : MonoBehaviour
{
    public Vector2Reference aimPosition;

    private void Update()
    {
        transform.position = aimPosition.Value;
    }
}
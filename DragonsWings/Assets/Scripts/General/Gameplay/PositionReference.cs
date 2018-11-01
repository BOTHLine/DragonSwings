using UnityEngine;

public class PositionReference : MonoBehaviour
{
    public Vector2Reference position;

    private void Update()
    {
        position.Variable.Value = transform.position;
    }
}
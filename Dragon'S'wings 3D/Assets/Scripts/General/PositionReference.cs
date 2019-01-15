using UnityEngine;

public class PositionReference : MonoBehaviour
{
    public Vector2Reference position;

    private void FixedUpdate() { position.Value = transform.position; }
}
using UnityEngine;

public class PositionReference : MonoBehaviour
{
    public Vector3Reference position;

    private void FixedUpdate() { position.Value = transform.position; }
}
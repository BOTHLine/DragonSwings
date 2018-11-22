using UnityEngine;

public class PositionReference : MonoBehaviour
{
    public Vector2Reference position;

    private void LateUpdate() { position.Value = transform.position; }
}
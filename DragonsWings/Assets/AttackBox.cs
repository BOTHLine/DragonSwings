using UnityEngine;

// AttackBox is the area where the Entity starts to Attack
public class AttackBox : MonoBehaviour
{
    public Color attackBoxColor;
    public Vector2Reference attackBoxSize;

    public Vector2Reference targetPosition;

    private void Update()
    {
        transform.rotation = Utils.GetLookAtRotation(transform.position, targetPosition, 90.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = attackBoxColor;
        Gizmos.DrawCube(Vector3.down * attackBoxSize.Value.y / 2.0f, attackBoxSize.Value);
    }
}
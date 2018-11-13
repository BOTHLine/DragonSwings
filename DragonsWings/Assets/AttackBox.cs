using UnityEngine;

// AttackBox is the area where the Entity starts to Attack
public class AttackBox : MonoBehaviour
{
    public Color attackBoxColor;
    public Vector2Reference attackBoxSize;

    public Vector2Reference targetPosition;

    public float testAngle = 0;

    private void Update()
    {
     //   transform.rotation = Utils.GetLookAtRotation(transform.position, targetPosition, 90.0f);
        transform.rotation = Utils.GetLookAtRotation(Vector2.down, testAngle);
        Vector2 center = (Vector2)transform.position + (targetPosition - (Vector2)transform.position).normalized * attackBoxSize.Value.y / 2.0f;
        float angle = Quaternion.Angle(Utils.GetLookAtRotation(transform.position, targetPosition), Quaternion.identity);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position - new Vector3(0.0f, attackBoxSize.Value.y / 2.0f), attackBoxSize, testAngle + 90, LayerList.CreateLayerMask
            (gameObject.layer));

        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    Debug.Log("Found Player");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = attackBoxColor;
        Gizmos.DrawCube(Vector3.down * attackBoxSize.Value.y / 2.0f, attackBoxSize.Value);
    }
}
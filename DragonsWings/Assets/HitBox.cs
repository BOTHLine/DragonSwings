using UnityEngine;

// HitBox is the area where this entities damages others
public class HitBox : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Color hitBoxColor;
    public Vector2Reference hitBoxSize;

    public Vector2Reference targetPosition;

    public FloatReference damage;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = hitBoxColor;
        Gizmos.DrawCube(Vector3.down * hitBoxSize.Value.y / 2.0f, hitBoxSize.Value);
    }

    public void AttackStart()
    {
        transform.rotation = Utils.GetLookAtRotation(transform.position, targetPosition, 90.0f);
        spriteRenderer.enabled = true;
    }

    public void Attack()
    {
        Vector2 directionVector = (transform.rotation * Vector2.down).normalized;
        Vector2 center = (Vector2)transform.position + (directionVector * hitBoxSize.Value.y);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, hitBoxSize, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                HurtBox hurtBox = collider.GetComponent<HurtBox>();
                if (hurtBox != null && hurtBox.transform.parent != transform.parent)
                    hurtBox.Hurt(damage);
            }
        }
        spriteRenderer.enabled = false;
    }
}
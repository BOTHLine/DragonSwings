using UnityEngine;

// HitBox is the area where this entities damages others
public class HitBox : MonoBehaviour
{
    public Color hitBoxColor;
    public Vector2Reference hitBoxSize;

    public Vector2Reference targetPosition;

    public FloatReference damage;

    private System.Collections.Generic.List<HurtBox> hurtBoxes = new System.Collections.Generic.List<HurtBox>();

    private void Update()
    {
        transform.rotation = Utils.GetLookAtRotation(transform.position, targetPosition, 90.0f);
    }

    public void Attack()
    {
        foreach (HurtBox hurtBox in hurtBoxes)
        {
            hurtBox.Hurt(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = hitBoxColor;
        Gizmos.DrawCube(Vector3.down * hitBoxSize.Value.y / 2.0f, hitBoxSize.Value);
    }
}
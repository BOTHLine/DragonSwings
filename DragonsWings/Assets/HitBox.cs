using UnityEngine;

// HitBox is the area where this entities damages others
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class HitBox : MonoBehaviour
{
    public FloatReference damage;

    private System.Collections.Generic.List<HurtBox> hurtBoxes = new System.Collections.Generic.List<HurtBox>();

    public GameEvent OnTargetInHitBox;

    public void Attack()
    {
        foreach (HurtBox hurtBox in hurtBoxes)
        {
            hurtBox.Hurt(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HurtBox")
        {
            hurtBoxes.Add(collision.GetComponent<HurtBox>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "HurtBox")
        {
            hurtBoxes.Remove(collision.GetComponent<HurtBox>());
        }
    }
}
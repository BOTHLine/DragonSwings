using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D _Rigidbody2D;
    [HideInInspector] public CircleCollider2D _CircleCollider2D;

    public FloatReference _Speed;
    public FloatReference _Damage;

    private void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _CircleCollider2D = GetComponentInChildren<CircleCollider2D>();
    }

    // TODO Destroy new Vases again (In Trigger, since the Projectile now only has a HitBox instead of a PushBox
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyableVase vase = collision.collider.GetComponent<DestroyableVase>();
        vase?.takeDmg();
        DestroyProjectile();
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HurtBox hurtBox = collision.GetComponentInSiblings<HurtBox>();
        hurtBox?.Hurt(_Damage);
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 direction)
    {
        _Rigidbody2D.velocity = direction * _Speed;
    }
}
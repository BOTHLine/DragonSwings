using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [HideInInspector] public Rigidbody _Rigidbody;
    [HideInInspector] public SphereCollider _SphereCollider;

    public FloatReference _Speed;
    public FloatReference _Damage;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        _SphereCollider = GetComponentInChildren<SphereCollider>();
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

    private void OnTriggerEnter(Collider collision)
    {
        HurtBox hurtBox = collision.GetComponentInSiblings<HurtBox>();
        hurtBox?.Hurt(_Damage.Value);
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector3 direction)
    {
        _Rigidbody.velocity = direction * _Speed.Value;
    }
}
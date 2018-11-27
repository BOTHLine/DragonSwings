using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Hook : MonoBehaviour
{
    // Components
    private Rigidbody2D _Rigidbody2D;
    private CircleCollider2D _CircleCollider2D;
    private SpriteRenderer _SpriteRenderer;

    private HookAbility _HookAbility;

    // Mono Behaviour
    private void Awake()
    {
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        _CircleCollider2D = GetComponentInChildren<CircleCollider2D>();

        _SpriteRenderer.enabled = false;
        _CircleCollider2D.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _HookAbility.HookHitSomething(collision);
        /*
        HookInteraction hookInteraction = collision.collider.GetComponentInSiblings<HookInteraction>();
        if (hookInteraction != null)
        {
            switch (hookInteraction._Weight)
            {
                case Weight.Light:
                    OnHookHitLightHookable.Raise();
                    break;
                case Weight.Medium:
                    OnHookHitMediumHookable.Raise();
                    break;
                case Weight.Heavy:
                    OnHookHitHeavyHookable.Raise();
                    break;
            }
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0.0f;

            hookInteraction.HitByHook(this);
        }
        else
        {
            OnHookHitNotHookable.Raise();
        }
        */
        Hookable hookable = collision.collider.GetComponentInParent<Hookable>();
        if (hookable != null)
        {
            switch (hookable.Weight)
            {
                case Weight.Light:
                    OnHookHitLightHookable.Raise();
                    break;
                case Weight.Medium:
                    OnHookHitMediumHookable.Raise();
                    break;
                case Weight.Heavy:
                    OnHookHitHeavyHookable.Raise();
                    break;
            }

            _Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            _Rigidbody2D.velocity = Vector2.zero;
            _Rigidbody2D.angularVelocity = 0.0f;

            hookable.OnHookHit();
        }
        else
        {
            OnHookHitNotHookable.Raise();
        }
    }

    // Methods
    public void Shoot(HookAbility hookAbility, Vector2 targetPosition, float hookSpeed)
    {
        transform.parent = null;
        transform.position = hookAbility.transform.position;
        transform.LookAt2D(targetPosition, -90.0f);

        _CircleCollider2D.enabled = true;
        _SpriteRenderer.enabled = true;

        _Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _Rigidbody2D.velocity = (targetPosition - (Vector2)transform.position).normalized * hookSpeed;
    }

    public void Reset()
    {
        _CircleCollider2D.enabled = false;
        _SpriteRenderer.enabled = false;
        _Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _Rigidbody2D.velocity = Vector2.zero;
        _Rigidbody2D.angularVelocity = 0.0f;

        transform.parent = _HookAbility.transform;
        transform.localPosition = Vector2.zero;
    }
}
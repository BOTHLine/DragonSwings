using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Hook : MonoBehaviour
{
    // Components
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody2D;
    private CircleCollider2D circleCollider2D;

    // Variables
    [SerializeField] private FloatReference hookRange;
    [SerializeField] private FloatReference hookSpeed;

    [SerializeField] private FloatReference _Damage;

    [SerializeField] private Vector2Reference startPosition;
    [SerializeField] private Vector2Reference targetPosition;

    [SerializeField] private BoolReference playerHasSomethingInHand;

    private Vector2 start;
    private bool canShoot = true;
    private Transform parent;

    //Events
    [SerializeField] private GameEvent OnHookShoot;
    [SerializeField] private GameEvent OnHookHitLightHookable;
    [SerializeField] private GameEvent OnHookHitMediumHookable;
    [SerializeField] private GameEvent OnHookHitHeavyHookable;
    [SerializeField] private GameEvent OnHookHitNotHookable;
    [SerializeField] private GameEvent OnHookReset;

    // Mono Behaviour
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        spriteRenderer.enabled = false;
        circleCollider2D.enabled = false;

        parent = transform.parent;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, start) > hookRange)
            ResetHook();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        /*
        Hookable hookable = collision.collider.GetComponent<Hookable>();
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

            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0.0f;

            hookable.OnHookHit();
        }
        else
        {
            OnHookHitNotHookable.Raise();
        }
        */
    }

    // Methods
    public void Shoot()
    {
        if (!canShoot || targetPosition.Value.Equals(startPosition.Value) || playerHasSomethingInHand.Value)
            return;

        canShoot = false;
        transform.parent = parent.parent;
        start = startPosition.Value;

        OnHookShoot.Raise();

        transform.rotation = Utils.GetLookAtRotation(targetPosition, -90.0f);

        circleCollider2D.enabled = true;
        spriteRenderer.enabled = true;

        Vector2 hookDirection = (targetPosition - startPosition).normalized;

        //rigidbody2D.MovePosition(startPosition);
        rigidbody2D.transform.position = startPosition.Value;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.velocity = hookDirection * hookSpeed;
    }

    public void ResetHook()
    {
        circleCollider2D.enabled = false;
        spriteRenderer.enabled = false;
        rigidbody2D.velocity = Vector2.zero;
        transform.parent = parent;
        transform.localPosition = Vector2.zero;
        canShoot = true;
        OnHookReset.Raise();
    }
}
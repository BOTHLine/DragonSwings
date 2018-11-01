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

    [SerializeField] private Vector2Reference startPosition;
    [SerializeField] private Vector2Reference targetPosition;

    private Vector2 start;
    private bool canShoot = true;
    private Transform parent;

    //Events
    [SerializeField] private GameEvent OnHookShoot;
    [SerializeField] private GameEvent OnHookHitLightHookable;
    [SerializeField] private GameEvent OnHookHitMediumHookable;
    [SerializeField] private GameEvent OnHookHitHeavyHookable;

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
        }

        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0.0f;
    }

    // Methods
    public void Shoot()
    {
        if (!canShoot || targetPosition.Value.Equals(startPosition.Value))
            return;

        canShoot = false;
        transform.parent = parent.parent;
        start = startPosition.Value;

        OnHookShoot.Raise();

        LookAt(targetPosition);

        circleCollider2D.enabled = true;
        spriteRenderer.enabled = true;

        rigidbody2D.MovePosition(startPosition);
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.velocity = (targetPosition - startPosition).normalized * hookSpeed;
    }

    public void ResetHook()
    {
        circleCollider2D.enabled = false;
        spriteRenderer.enabled = false;
        rigidbody2D.velocity = Vector2.zero;
        transform.parent = parent;
        transform.localPosition = Vector2.zero;
        canShoot = true;
    }

    private void LookAt(Vector2 targetPosition)
    {
        Vector2 targetDirection = (targetPosition - (Vector2)transform.position).normalized;
        float zRotation = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, zRotation - 90.0f);
    }
}
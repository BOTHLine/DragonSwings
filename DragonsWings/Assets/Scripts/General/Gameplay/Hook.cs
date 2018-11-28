using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hook : MonoBehaviour
{
    // Components
    private Rigidbody2D _Rigidbody2D;
    private CircleCollider2D _CircleCollider2D;
    private SpriteRenderer _SpriteRenderer;

    private LookForward _LookForward;

    // Variables
    private HookAbility _HookAbility;

    private float _HookSpeed;
    private bool flyingBack;

    // Mono Behaviour
    private void Awake()
    {
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        _CircleCollider2D = GetComponentInChildren<CircleCollider2D>();

        _LookForward = GetComponentInChildren<LookForward>();

        _SpriteRenderer.enabled = false;
        _CircleCollider2D.enabled = false;
    }

    private void Update()
    {
        if (flyingBack)
        {
            Vector2 targetVector = (Vector2)_HookAbility.transform.position - (Vector2)transform.position;
            _Rigidbody2D.velocity = targetVector.normalized * _HookSpeed;

            if (targetVector.sqrMagnitude <= 0.1f)
            {
                _HookAbility.HookReachedPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { _HookAbility.HookHitSomething(collision); }

    // Methods
    public void Initialize(HookAbility hookAbility, float hookSpeed)
    {
        _HookAbility = hookAbility;
        _HookSpeed = hookSpeed;
    }

    public void Shoot(Vector2 targetPosition)
    {
        flyingBack = false;
        _LookForward.correctionValue -= 180;

        transform.parent = null;
        transform.position = _HookAbility.transform.position;
        transform.LookAt2D(targetPosition, -90.0f);

        _CircleCollider2D.enabled = true;
        _SpriteRenderer.enabled = true;

        _Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _Rigidbody2D.velocity = (targetPosition - (Vector2)transform.position).normalized * _HookSpeed;
    }

    public void FlyBack()
    {
        flyingBack = true;
        _LookForward.correctionValue += 180;
        _CircleCollider2D.enabled = false;
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
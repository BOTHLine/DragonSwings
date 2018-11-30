using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hook : MonoBehaviour
{
    // Components
    private Rigidbody2D _Rigidbody2D;
    private PushBox _PushBox;
    private SpriteRenderer _SpriteRenderer;

    private LookForward _LookForward;

    // Variables
    private HookAbility _HookAbility;

    private float _HookSpeed;
    private bool _FlyingBack;

    private float _OldMass;
    private float _OldDrag;

    private HookResponder _AttachedHookResponder;

    // Mono Behaviour
    private void Awake()
    {
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        _PushBox = GetComponentInChildren<PushBox>();

        _LookForward = GetComponentInChildren<LookForward>();

        _SpriteRenderer.enabled = false;
        _PushBox.gameObject.SetActive(false);

        _OldMass = _Rigidbody2D.mass;
        _OldDrag = _Rigidbody2D.drag;
    }

    private void FixedUpdate()
    {
        if (_FlyingBack)
        {
            Vector2 targetVector = (Vector2)_HookAbility.transform.position - (Vector2)transform.position;
            _Rigidbody2D.velocity = targetVector.normalized * (_AttachedHookResponder == null ? _HookSpeed : _HookSpeed / 2.0f);

            if (targetVector.sqrMagnitude <= (_Rigidbody2D.velocity * Time.fixedDeltaTime * 2.0f).sqrMagnitude)
            { _HookAbility.HookReachedPlayer(); }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _HookAbility.HookHitSomething(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _HookAbility.HookHitSomething(collision);
    }

    // Methods
    public void Initialize(HookAbility hookAbility, float hookSpeed)
    {
        _HookAbility = hookAbility;
        _HookSpeed = hookSpeed;
    }

    public void Shoot(Vector2 targetPosition)
    {
        _LookForward.correctionValue -= 180;

        transform.parent = null;
        transform.position = _HookAbility.transform.position;
        transform.LookAt2D(targetPosition, -90.0f);

        _PushBox.gameObject.SetActive(true);
        _SpriteRenderer.enabled = true;

        _Rigidbody2D.velocity = (targetPosition - (Vector2)transform.position).normalized * _HookSpeed;
    }

    public void ResetVelocity()
    { _Rigidbody2D.velocity = _Rigidbody2D.velocity.normalized * _HookSpeed; }

    public void StopHook()
    { _Rigidbody2D.velocity = Vector2.zero; }

    public void FlyBack()
    {
        _FlyingBack = true;
        _LookForward.correctionValue += 180;
        _PushBox.gameObject.SetActive(false);
    }

    public void AttachHookResponder(HookResponder hookResponder)
    {
        _AttachedHookResponder = hookResponder;
        hookResponder.AttachToObject(transform);

        _Rigidbody2D.mass = hookResponder._Rigidbody2D.mass;
        _Rigidbody2D.drag = hookResponder._Rigidbody2D.drag;
    }

    public HookResponder DetachHookResponder()
    {
        HookResponder hookResponder = _AttachedHookResponder;
        hookResponder?.DetachFromObject();
        _AttachedHookResponder = null;

        _Rigidbody2D.mass = _OldMass;
        _Rigidbody2D.drag = _OldDrag;

        return hookResponder;
    }

    public void Reset()
    {
        _FlyingBack = false;
        _PushBox.gameObject.SetActive(false);
        _SpriteRenderer.enabled = false;
        _Rigidbody2D.velocity = Vector2.zero;
        _Rigidbody2D.angularVelocity = 0.0f;

        transform.parent = _HookAbility.transform;
        transform.localPosition = Vector2.zero;
    }
}
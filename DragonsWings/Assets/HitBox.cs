using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
    // Components
    private Collider2D _Collider2D;
    private SpriteRenderer _SpriteRenderer;

    // References
    public Vector2Reference _TargetPosition;

    public BoolReference _IsAttackStarting;
    public BoolReference _IsAttacking;

    // Variables
    public FloatReference _Damage;

    private Collider2D[] _Collider2Ds;

    // Mono Behaviour

    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (_IsAttackStarting.Value)
        {
            AttackStart();
            _IsAttackStarting.Value = false;
        }
        if (_IsAttacking.Value)
        {
            Attack();
            _IsAttacking.Value = false;
        }
    }

    // Méthods
    public void AttackStart()
    {
        transform.LookAt2D(_TargetPosition, -90.0f);
        _SpriteRenderer.enabled = true;
    }

    public void Attack()
    {
        int amount = _Collider2D.OverlapColliderWithOwnLayerMask(_Collider2Ds);

        Debug.Log("HitBox Amount: " + amount);

        for (int i = 0; i < amount; i++)
        { _Collider2Ds[i].GetComponent<HurtBox>()?.Hurt(_Damage); }

        _SpriteRenderer.enabled = false;
    }

    // Debug
    public Color _DebugColor;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = _DebugColor;
        if (_Collider2D is BoxCollider2D)
        {
            BoxCollider2D boxCollider2D = _Collider2D as BoxCollider2D;
            Gizmos.DrawCube(boxCollider2D.offset, new Vector2(boxCollider2D.size.x, boxCollider2D.size.y));
        }
    }
}
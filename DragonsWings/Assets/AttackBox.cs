using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackBox : MonoBehaviour
{
    // Components
    [HideInInspector] public Collider2D _Collider2D;

    // References
    public Vector2Reference _TargetPosition;

    public BoolReference _IsPlayerInAttackRange;

    // Variables
    private Collider2D[] _Collider2Ds;

    // Mono Behaviour
    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
        _Collider2Ds = new Collider2D[50];
    }

    private void Update()
    {
        transform.LookAt2D(_TargetPosition, -90.0f);

        int amount = _Collider2D.OverlapColliderWithOwnLayerMask(_Collider2Ds);

        for (int i = 0; i < amount; i++)
        {
            if (_Collider2Ds[i].transform.parent.tag == "Player")
            {
                _IsPlayerInAttackRange.Value = true;
                return;
            }
        }
        _IsPlayerInAttackRange.Value = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { if (collision.transform.parent.tag == "Player") { _IsPlayerInAttackRange.Value = true; } }

    private void OnTriggerExit2D(Collider2D collision)
    { if (collision.transform.parent.tag == "Player") { _IsPlayerInAttackRange.Value = false; } }

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
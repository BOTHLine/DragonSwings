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

    // Mono Behaviour
    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        transform.LookAt2D(_TargetPosition.Value, -90.0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    { if (collision.transform.parent.tag == "Player") { _IsPlayerInAttackRange.Value = true; } }

    private void OnTriggerExit2D(Collider2D collision)
    { if (collision.transform.parent.tag == "Player") { _IsPlayerInAttackRange.Value = false; } }


    // Debug
    public Color _DebugColor;

#if UNITY_EDITOR
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
#endif
}
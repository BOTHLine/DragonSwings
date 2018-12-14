using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
    // Components
    private Collider2D _Collider2D;

    public SpriteRenderer _Indicator;
    public SpriteRenderer _Slash;

    // References
    public Vector2Reference _TargetPosition;

    // Variables
    public FloatReference _Damage;
    public FloatReference _AttackTime;

    private Collider2D[] _Collider2Ds;

    private System.Collections.Generic.List<HurtBox> _HurtBoxesInRange;

    // Events
    public GameEventMap _OnAttackStart;
    public GameEventMap _OnAttackFinishRaise;

    // Mono Behaviour

    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
        _HurtBoxesInRange = new System.Collections.Generic.List<HurtBox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { AddHurtBox(collision.GetComponentInSiblings<HurtBox>()); }

    private void OnTriggerExit2D(Collider2D collision)
    { RemoveHurtBox(collision.GetComponentInSiblings<HurtBox>()); }

    // Méthods
    public void AttackStart()
    {
        _OnAttackStart.Raise(transform.parent.gameObject);

        transform.LookAt2D(_TargetPosition.Value, -90.0f);
        _Indicator.enabled = true;

        System.Collections.IEnumerator AttackCoroutine = Attack(_AttackTime.Value);
        StartCoroutine(AttackCoroutine);
    }

    public System.Collections.IEnumerator Attack(float time)
    {
        yield return new WaitForSeconds(time);

        _OnAttackFinishRaise.Raise(transform.parent.gameObject);

        foreach (HurtBox hurtBox in _HurtBoxesInRange)
        { hurtBox.Hurt(_Damage.Value); }

        _Indicator.enabled = false;
        _Slash.enabled = true;

        System.Collections.IEnumerator DisableSlashRendererCoroutine = DisableSlashRenderer(0.1f);
        StartCoroutine(DisableSlashRendererCoroutine);
    }

    private void AddHurtBox(HurtBox hurtBox)
    {
        if (hurtBox == null) return;
        if (_HurtBoxesInRange.Contains(hurtBox)) return;
        _HurtBoxesInRange.Add(hurtBox);
        Debug.Log("Added Hurtbox: " + hurtBox.transform.parent.name);
    }

    private void RemoveHurtBox(HurtBox hurtBox)
    {
        if (hurtBox == null) return;
        _HurtBoxesInRange.Remove(hurtBox);
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

    private System.Collections.IEnumerator DisableSlashRenderer(float time)
    {
        yield return new WaitForSeconds(time);
        _Slash.enabled = false;
    }
}
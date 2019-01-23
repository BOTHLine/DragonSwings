using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour
{
    // Components
    private Collider _Collider;

    public SpriteRenderer _Indicator;
    public SpriteRenderer _Slash;

    // References
    public Vector3Reference _TargetPosition;

    // Variables
    public FloatReference _Damage;
    public FloatReference _AttackTime;

    private System.Collections.Generic.List<HurtBox> _HurtBoxesInRange;

    // Events
    public GameEventMap _OnAttackStart;
    public GameEventMap _OnAttackFinishRaise;

    // Mono Behaviour

    private void Awake()
    {
        _Collider = GetComponent<Collider>();
        _HurtBoxesInRange = new System.Collections.Generic.List<HurtBox>();
    }

    private void OnTriggerEnter(Collider collision)
    { AddHurtBox(collision.GetComponentInSiblings<HurtBox>()); }

    private void OnTriggerExit(Collider collision)
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
        if (_Collider is BoxCollider)
        {
            BoxCollider boxCollider = _Collider as BoxCollider;
            Gizmos.DrawCube(boxCollider.center, boxCollider.size);
        }
    }

    private System.Collections.IEnumerator DisableSlashRenderer(float time)
    {
        yield return new WaitForSeconds(time);
        _Slash.enabled = false;
    }
}
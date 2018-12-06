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

    public BoolReference _IsAttackStarting;
    public BoolReference _IsAttacking;

    // Variables
    public FloatReference _Damage;

    private Collider2D[] _Collider2Ds;

    private System.Collections.Generic.List<HurtBox> _HurtBoxesInRange;

    // Mono Behaviour

    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
        _HurtBoxesInRange = new System.Collections.Generic.List<HurtBox>();
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
            //    Debug.Log("Attack Call: " + transform.parent.name);
            Attack();
            _IsAttacking.Value = false;
        }

        /*
        if (_HurtBoxesInRange.Count > 0)
        {
            Debug.Log("Hurtboxes for Enemy: " + transform.parent.name);
            for (int i = 0; i < _HurtBoxesInRange.Count; i++)
            {
                Debug.Log(_HurtBoxesInRange[i] + " of parent: " + _HurtBoxesInRange[i].transform.parent.name);
            }
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { AddHurtBox(collision.GetComponentInSiblings<HurtBox>()); }

    private void OnTriggerExit2D(Collider2D collision)
    { RemoveHurtBox(collision.GetComponentInSiblings<HurtBox>()); }

    // Méthods
    public void AttackStart()
    {
        transform.LookAt2D(_TargetPosition, -90.0f);
        _Indicator.enabled = true;
    }

    public void Attack()
    {
        //   Debug.Log("Attack: " + transform.parent.name);
        /*
        int amount = _Collider2D.OverlapColliderWithOwnLayerMask(_Collider2Ds);

        for (int i = 0; i < amount; i++)
        { _Collider2Ds[i].GetComponent<HurtBox>()?.Hurt(_Damage); }
        */
        foreach (HurtBox hurtBox in _HurtBoxesInRange)
        { hurtBox.Hurt(_Damage); }

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
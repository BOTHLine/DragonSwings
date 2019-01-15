using UnityEngine;

public class AttackShoot : MonoBehaviour
{
    // Components
    public Projectile _PrjectilePrefab;

    public Transform _ProjectileSpawnPosition;

    // References
    public Vector2Reference _TargetPosition;

    public FloatReference _AttackTime;

    // Events
    public GameEventMap _OnAttackStart;
    public GameEventMap _OnAttackFinishRaise;

    // Mono Behaviour

    // Methods
    public void AttackStart()
    {
        _OnAttackStart.Raise(transform.parent.gameObject);

        transform.LookAt2D(_TargetPosition.Value, -90.0f);

        System.Collections.IEnumerator AttackCoroutine = Attack(_AttackTime.Value);
        StartCoroutine(AttackCoroutine);
    }

    public System.Collections.IEnumerator Attack(float time)
    {
        yield return new WaitForSeconds(time);

        _OnAttackFinishRaise.Raise(transform.parent.gameObject);

        Vector2 shootDirection = _TargetPosition.Value - (Vector2)transform.position;
        Projectile projectile = Instantiate(_PrjectilePrefab, _ProjectileSpawnPosition.position, Quaternion.identity, null);
        projectile.SetDirection(shootDirection);
    }
}
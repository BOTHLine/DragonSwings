using UnityEngine;

public class AbilityShoot : MonoBehaviour
{
    public Transform _SpawnPositionTransform;

    public Vector2Reference _TargetPosition;

    public Projectile _ProjectilePrefab;

    public float _LookAtCorrectionValue;

    public GameEventMap _OnAttackStart;
    public GameEventMap _OnAttackFinishRaise;

    public void Shoot()
    {
        _OnAttackStart.Raise(transform.parent.gameObject);

        LookAtTargetPosition();
        Projectile projectile = Instantiate(_ProjectilePrefab, _SpawnPositionTransform.position, Quaternion.identity);
        projectile.SetDirection((_TargetPosition.Value - (Vector2)_SpawnPositionTransform.position).normalized);

        _OnAttackFinishRaise.Raise(transform.parent.gameObject);
    }

    private void LookAtTargetPosition()
    { transform.LookAt2D(_TargetPosition.Value, _LookAtCorrectionValue); }
}
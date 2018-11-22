using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Shoot Action")]
public class ShootAction : Action
{
    public Vector2Reference _TargetPosition;

    public FloatReference _AttackTime;
    private float _CurrentAttackTime;

    public FloatReference _AttackCooldown;
    public FloatReference _CurrentAttackCooldown;

    public Projectile _ProjectilePrefab;

    public GameEvent _OnAttackStart;
    public GameEvent _OnAttack;

    public override void Act(StateController controller)
    {
        _CurrentAttackTime += Time.deltaTime;
        _CurrentAttackCooldown.ChangeValue(-Time.deltaTime, controller.gameObject);

        if (_CurrentAttackTime >= _AttackTime.Get(controller.gameObject) && _CurrentAttackCooldown <= 0.0f)
        { Attack(controller); }
    }

    public override void EnterState(StateController controller)
    { _CurrentAttackTime = 0.0f; }

    private void Attack(StateController controller)
    {
        Projectile projectile = Instantiate(_ProjectilePrefab, controller.transform.position, Quaternion.identity, null);
        projectile.SetDirection((_TargetPosition.Get(controller.gameObject) - (Vector2)controller.transform.position).normalized);
        _CurrentAttackTime = 0.0f;
        _CurrentAttackCooldown.Set(_AttackCooldown.Get(controller.gameObject), controller.gameObject);
        _OnAttack.Raise();
    }
}
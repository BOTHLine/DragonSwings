using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Shoot Action")]
public class ShootAction : Action
{
    public Vector2Reference _TargetPosition;

    //public FloatReference _AttackTime;
    // private float _CurrentAttackTime;

    public FloatReference _AttackCooldown;
    public FloatMap _CurrentAttackCoolDownMap;
    // public FloatReference _CurrentAttackCooldown;

    public Projectile _ProjectilePrefab;

    public override void Act(StateController controller)
    {
        //   _CurrentAttackTime += Time.deltaTime;
        // _CurrentAttackCooldown.ChangeValue(-Time.deltaTime, controller.gameObject);

        //    if (_CurrentAttackTime >= _AttackTime.Get(controller.gameObject) && _CurrentAttackCooldown <= 0.0f)
        //   { Attack(controller); }
    }

    // public override void EnterState(StateController controller)
    // { _CurrentAttackTime = 0.0f; }

    public override void ExitState(StateController controller)
    { Attack(controller); }

    private void Attack(StateController controller)
    {
        Vector2 shootDirection = (_TargetPosition.Get(controller.gameObject) - (Vector2)controller.transform.position).normalized;
        Projectile projectile = Instantiate(_ProjectilePrefab, (Vector2)controller.transform.position + shootDirection * 0.1f, Quaternion.identity, null);
        projectile.SetDirection(shootDirection);
        //   _CurrentAttackTime = 0.0f;
        _CurrentAttackCoolDownMap.Set(controller.gameObject, _AttackCooldown.Get(controller.gameObject));
    }
}
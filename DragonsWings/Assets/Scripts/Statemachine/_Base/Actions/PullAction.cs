using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Pull Action")]
public class PullAction : Action
{
    public FloatReference pullSpeed;
    public Vector2Reference playerPosition;
    public Vector2Reference hookPosition;

    public FloatReference _DamageCircleRadius;
    public FloatReference _PullDamage;

    public FloatReference distanceThreshold;

    private System.Collections.Generic.List<HurtBox> _AlreadyDamagedHurtBoxes;

    public GameEvent OnPullFinished;

    public override void Act(StateController controller)
    {
        controller.rigidbody2D.velocity = (hookPosition - playerPosition).normalized * pullSpeed;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(controller.transform.position, _DamageCircleRadius, LayerList.PlayerAttack.LayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            HurtBox hurtBox = colliders[i].GetComponentInSiblings<HurtBox>();
            if (hurtBox != null && !_AlreadyDamagedHurtBoxes.Contains(hurtBox))
            {
                hurtBox.Hurt(_PullDamage);
                _AlreadyDamagedHurtBoxes.Add(hurtBox);
                continue;
            }

            Projectile projectile = colliders[i].GetComponent<Projectile>();
            if (projectile != null)
            {
                Destroy(projectile.gameObject);
            }
        }

        if ((hookPosition - playerPosition).sqrMagnitude <= distanceThreshold * distanceThreshold)
            OnPullFinished.Raise();
    }

    public override void EnterState(StateController controller)
    { _AlreadyDamagedHurtBoxes = new System.Collections.Generic.List<HurtBox>(); }

    public override void ExitState(StateController controller)
    { controller.rigidbody2D.velocity = Vector2.zero; }
}
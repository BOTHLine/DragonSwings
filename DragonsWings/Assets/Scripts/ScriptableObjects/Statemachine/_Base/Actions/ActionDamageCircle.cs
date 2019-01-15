using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Damage Circle")]
public class ActionDamageCircle : Action
{
    public FloatReference _Radius;
    public FloatReference _Damage;

    public HurtBoxListRuntimeSet _AlreadyDamagedHurtBoxes;

    public override void Act(StateController controller)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(controller.transform.position, _Radius.Get(controller.gameObject), LayerList.PlayerAttack.LayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            HurtBox hurtBox = colliders[i].GetComponentInSiblings<HurtBox>();
            if (hurtBox != null && !_AlreadyDamagedHurtBoxes.Contains(hurtBox))
            {
                hurtBox.Hurt(_Damage.Get(controller.gameObject));
                _AlreadyDamagedHurtBoxes.Add(hurtBox);
                continue;
            }

            Projectile projectile = colliders[i].GetComponent<Projectile>();
            if (projectile != null)
            {
                Destroy(projectile.gameObject);
            }
        }
    }
}
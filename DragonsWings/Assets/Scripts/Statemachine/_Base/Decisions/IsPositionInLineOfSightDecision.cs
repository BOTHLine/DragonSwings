using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/IsPositionInLineOfSight Decision")]
public class IsPositionInLineOfSightDecision : Decision
{
    public Vector2Reference _TargetPosition;

    public override bool Decide(StateController controller)
    {
        Vector2 direction = _TargetPosition.Get(controller.gameObject) - (Vector2)controller.transform.position;
        bool hitTriggers = Physics2D.queriesHitTriggers;
        Physics2D.queriesHitTriggers = false;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(controller.transform.position, direction, direction.magnitude, LayerList.EnemyProjectile.LayerMask);
        Physics2D.queriesHitTriggers = hitTriggers;
        return raycastHit2D.collider == null;
    }
}
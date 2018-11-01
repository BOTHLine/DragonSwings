using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Collide Decision")]
public class CollideDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        Collider2D collider = Physics2D.OverlapCircle(controller.transform.position, controller.circleCollider2D.radius, LayerList.CreateLayerMask(controller.gameObject.layer));
        return collider != null;
    }

    public override void EnterState(StateController controller) { }
    public override void ExitState(StateController controller) { }
}
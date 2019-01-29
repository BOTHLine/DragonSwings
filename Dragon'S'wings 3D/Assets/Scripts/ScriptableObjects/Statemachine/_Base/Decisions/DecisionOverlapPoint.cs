using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Overlap Point")]
public class DecisionOverlapPoint : Decision
{
    public LayerMask _LayerMask;

    public override bool Decide(StateController controller)
    {
        // Collider2D coll = Physics2D.OverlapPoint(controller.transform.position, _LayerMask);
        return Physics.Raycast(controller.transform.position, Vector3.down, 10.0f, _LayerMask);
    }
}
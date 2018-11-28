using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/IsPositionInLineOfSight Decision")]
public class IsPositionInLineOfSightDecision : Decision
{
    public Vector2Reference _TargetPosition;

    public LayerMask _LayerMask;
    public string[] _IgnoreTags;

    public override bool Decide(StateController controller)
    {
        Vector2 direction = _TargetPosition.Get(controller.gameObject) - (Vector2)controller.transform.position;
        RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(controller.transform.position, direction, direction.magnitude, _LayerMask);
        for (int i = 0; i < raycastHit2D.Length; i++)
        {
            bool tagIncluded = false;
            for (int j = 0; j < _IgnoreTags.Length; j++)
            {
                if (_IgnoreTags[j].Equals(raycastHit2D[i].collider.tag))
                {
                    tagIncluded = true;
                }
            }
            if (!tagIncluded) { return false; }
        }
        return true;
    }
}
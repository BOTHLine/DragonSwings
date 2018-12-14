using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Line of Sight")]
public class DecisionLineOfSight : Decision
{
    public Vector2Reference _TargetPosition;

    public string[] _IgnoreTags;

    public override bool Decide(StateController controller)
    {
        Vector2 direction = _TargetPosition.Get(controller.gameObject) - (Vector2)controller.transform.position;
        bool hitTriggers = Physics2D.queriesHitTriggers;
        Physics2D.queriesHitTriggers = false;
        RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(controller.transform.position, direction, direction.magnitude, LayerList.EnemyProjectile.LayerMask);
        for (int i = 0; i < raycastHit2D.Length; i++)
        {
            bool tagIncluded = false;
            for (int j = 0; j < _IgnoreTags.Length; j++)
            {
                if (_IgnoreTags[j].Equals(raycastHit2D[i].collider.tag))
                {
                    tagIncluded = true;
                    break;
                }
            }
            if (!tagIncluded) { return false; }
        }
        return true;
    }
}
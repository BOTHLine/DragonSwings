using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Line of Sight")]
public class DecisionLineOfSight : Decision
{
    public Vector3Reference _TargetPosition;

    public string[] _IgnoreTags;

    public override bool Decide(StateController controller)
    {
        Vector2 direction = _TargetPosition.Get(controller.gameObject) - controller.transform.position;
        bool hitTriggers = Physics.queriesHitTriggers;
        Physics.queriesHitTriggers = false;
        RaycastHit[] raycastHit = Physics.RaycastAll(controller.transform.position, direction, direction.magnitude, LayerList.EnemyProjectile.LayerMask);
        for (int i = 0; i < raycastHit.Length; i++)
        {
            bool tagIncluded = false;
            for (int j = 0; j < _IgnoreTags.Length; j++)
            {
                if (_IgnoreTags[j].Equals(raycastHit[i].collider.tag))
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
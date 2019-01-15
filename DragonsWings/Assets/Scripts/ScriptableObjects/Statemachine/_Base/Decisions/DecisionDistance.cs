using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Decisions/Distance")]
public class DecisionDistance : Decision
{
    public Vector2Reference _TargetPosition;
    public FloatReference _MaxDistance;

    public override bool Decide(StateController controller)
    {
        float squaredDistance = ((Vector2)controller.transform.position - _TargetPosition.Get(controller.gameObject)).sqrMagnitude;
        float squaredMaxDistance = Mathf.Pow(_MaxDistance.Get(controller.gameObject), 2);

        return squaredDistance <= squaredMaxDistance;
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Target To Vector3Complex")]
public class ActionTargetToVector3Complex : Action
{
    public Vector3Reference _TargetPosition;
    public Vector3ComplexMap _MoveMap;

    public override void Act(StateController controller)
    { _MoveMap.Set(controller.gameObject, new Vector3Complex(controller.transform.position, _TargetPosition.Get(controller.gameObject) - controller.transform.position)); }
}
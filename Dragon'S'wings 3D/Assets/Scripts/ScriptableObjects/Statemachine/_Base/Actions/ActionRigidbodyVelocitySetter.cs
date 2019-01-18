using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Rigidbody Velocity Setter")]
public class ActionRigidbodyVelocitySetter : Action
{
    public Vector3ComplexMap _MoveMap;
    public FloatReference _MoveSpeed;

    public override void Act(StateController controller)
    {
        Vector3 newVelocity = _MoveMap.Get(controller.gameObject).Direction * _MoveSpeed.Get(controller.gameObject);
        newVelocity.y = controller._Rigidbody.velocity.y;
        controller._Rigidbody.velocity = newVelocity;
    }
}
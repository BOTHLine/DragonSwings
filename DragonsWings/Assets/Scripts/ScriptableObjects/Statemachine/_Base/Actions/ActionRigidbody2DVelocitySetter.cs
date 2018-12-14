using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Rigidbody2D Velocity Setter")]
public class ActionRigidbody2DVelocitySetter : Action
{
    public Vector2ComplexMap _MoveMap;
    public FloatReference _MoveSpeed;

    public override void Act(StateController controller)
    { controller.rigidbody2D.velocity = _MoveMap.Get(controller.gameObject).Direction * _MoveSpeed.Get(controller.gameObject); }
}
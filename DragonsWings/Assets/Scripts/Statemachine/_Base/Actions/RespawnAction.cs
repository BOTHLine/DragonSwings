using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Respawn Action")]
public class RespawnAction : Action
{
    public Vector2Reference lastSavePosition;
    public FloatReference health;

    public GameEvent OnPlayerRespawn;

    public override void Act(StateController controller)
    {
        OnPlayerRespawn.Raise();
    }

    public override void EnterState(StateController controller) { }

    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
        //   controller.rigidbody2D.MovePosition(lastSavePosition);
        controller.transform.position = lastSavePosition.Value;
        controller.spriteRenderer.enabled = true;
    }
}
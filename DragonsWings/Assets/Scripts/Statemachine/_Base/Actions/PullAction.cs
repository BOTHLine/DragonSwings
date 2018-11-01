using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Pull Action")]
public class PullAction : Action
{
    public FloatReference pullSpeed;
    public Vector2Reference playerPosition;
    public Vector2Reference hookPosition;

    public FloatReference distanceThreshold;

    public GameEvent OnPullFinished;

    public override void Act(StateController controller)
    {
        controller.rigidbody2D.velocity = (hookPosition - playerPosition).normalized * pullSpeed;

        Debug.Log("Current distance: " + (hookPosition - playerPosition).sqrMagnitude + ", min distance: " + distanceThreshold * distanceThreshold);
        if ((hookPosition - playerPosition).sqrMagnitude <= distanceThreshold * distanceThreshold)
            OnPullFinished.Raise();
    }

    public override void EnterState(StateController controller)
    {
        controller.rigidbody2D.velocity = (hookPosition - playerPosition).normalized * pullSpeed;
    }

    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
    }
}
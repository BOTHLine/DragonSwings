using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Pull")]
public class ActionPull : Action
{
    public FloatReference _PullSpeed;
    public Vector2Reference _PlayerPosition;
    public Vector2Reference _HookPosition;

    public FloatReference _DistanceThreshold;

    public GameEvent _OnPullFinishRaise;
    public GameEvent _OnPullFinish;

    public override void Act(StateController controller)
    {
        controller.rigidbody2D.velocity = (_HookPosition.Get(controller.gameObject) - _PlayerPosition.Get(controller.gameObject)).normalized * _PullSpeed.Get(controller.gameObject);

        if ((_HookPosition - _PlayerPosition).sqrMagnitude <= _DistanceThreshold * _DistanceThreshold)
            _OnPullFinishRaise.Raise();
    }

    /*
    public override void ExitState(StateController controller)
    {
        controller.rigidbody2D.velocity = Vector2.zero;
        _OnPullFinish.Raise();
    }
    */
}
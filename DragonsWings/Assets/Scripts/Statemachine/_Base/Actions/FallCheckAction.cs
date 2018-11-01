using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/FallCheck Action")]
public class FallCheckAction : Action
{
    public FloatReference fallTime;
    private float currentFallTime;

    public GameEvent OnPlayerFall;
    public GameEvent OnPlayerLand;

    public override void Act(StateController controller)
    {
        Collider2D coll = Physics2D.OverlapPoint(controller.transform.position, LayerList.FallCheck.LayerMask);
        if (coll == null)
        {
            OnPlayerLand.Raise();
            return;
        }

        currentFallTime += Time.deltaTime;
        if (currentFallTime >= fallTime)
        {
            OnPlayerFall.Raise();
        }
    }

    public override void EnterState(StateController controller) { currentFallTime = 0.0f; }
    public override void ExitState(StateController controller) { }
}
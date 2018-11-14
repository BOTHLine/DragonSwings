using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Event Action")]
public class StartHookAction : Action
{
    private Hook hook;

    public GameEvent OnHookInput;

    private UnityEngine.Events.UnityEvent hookShootEvent;

    public override void Act(StateController controller) { }

    public override void EnterState(StateController controller)
    {
        hook = controller.hook;

        hookShootEvent = new UnityEngine.Events.UnityEvent { };
        hookShootEvent.AddListener(ShootHook);
        OnHookInput.RegisterListener(hookShootEvent);
    }

    public override void ExitState(StateController controller)
    {
        OnHookInput.UnregisterListener(hookShootEvent);
    }

    public void ShootHook()
    {
        hook.Shoot();
    }
}
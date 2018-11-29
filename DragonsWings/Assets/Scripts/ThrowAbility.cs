using UnityEngine;

public class ThrowAbility : MonoBehaviour
{
    // References
    public HookResponderVariable _HookResponder;

    public Vector2Reference _ThrowTargetPosition;

    // Variables

    // Events
    public GameEvent OnObjectPickedUp;
    public GameEvent OnObjectThrown;

    // Mono Behaviour

    // Methods
    public void ThrowObject()
    {
        HookResponder hookResponder = _HookResponder?.Value;
        if (hookResponder != null)
        {
            _HookResponder.Value = null;
            hookResponder.DetachFromObject();
            hookResponder.StartThrow(_ThrowTargetPosition);
            OnObjectThrown.Raise();
        }
    }
}
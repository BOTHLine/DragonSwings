using UnityEngine;

public class ThrowAbility : MonoBehaviour
{
    // References
    public HookResponderVariable _HookResponder;

    public Vector2Reference _ThrowTargetPosition;

    // Variables

    // Events
    public GameEvent OnObjectThrown;

    // Mono Behaviour

    private void OnEnable()
    { _HookResponder.Value.AttachToObject(transform); }

    // Methods
    public void ThrowObject()
    {
        _HookResponder.Value.DetachFromObject();
        _HookResponder.Value.StartThrow(_ThrowTargetPosition);
        _HookResponder.Value = null;
        OnObjectThrown.Raise();
    }
}
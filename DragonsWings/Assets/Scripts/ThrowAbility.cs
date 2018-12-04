using UnityEngine;

public class ThrowAbility : MonoBehaviour
{
    // References
    public HookResponderVariable _HookResponder;

    public Vector2Reference _TargetDirection;
    public Vector2Reference _TargetPosition;

    // Variables

    // Events
    public GameEvent OnObjectThrown;

    // Mono Behaviour
    private void OnEnable()
    { _HookResponder.Value.AttachToObject(transform); }

    private void Update()
    {
        if (!IsAiming()) { return; }

        ShowThrowParabola();
    }

    // Methods
    private bool IsAiming()
    {
        return (!_TargetDirection.Value.Equals(Vector2.zero));
    }

    private void ShowThrowParabola()
    {

    }

    public void ThrowObject()
    {
        if (!IsAiming()) { return; }

        _HookResponder.Value.StartThrow(_TargetPosition);
        _HookResponder.Value = null;
        OnObjectThrown.Raise();
    }
}
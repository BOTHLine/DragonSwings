using UnityEngine;

public class ThrowAbility : MonoBehaviour
{
    // References
    public HookResponderVariable _HookResponder;

    public Vector2Reference _TargetDirection;
    public Vector2Reference _TargetPosition;

    public FloatReference _FlyTime;
    public FloatReference _FlyHeight;

    public Transform _PickUpTargetPosition;

    // Variables

    // Events
    public GameEvent OnObjectThrown;

    // Mono Behaviour
    private void OnEnable()
    { _HookResponder.Value.AttachToObject(_PickUpTargetPosition); }

    // Methods
    public bool IsAiming()
    {
        return (!_TargetDirection.Value.Equals(Vector2.zero));
    }

    public void ThrowObject()
    {
        if (!IsAiming()) { return; }

        _HookResponder.Value.StartThrow(_TargetPosition, _FlyTime, _FlyHeight);
        _HookResponder.Value = null;
        OnObjectThrown.Raise();
    }
}
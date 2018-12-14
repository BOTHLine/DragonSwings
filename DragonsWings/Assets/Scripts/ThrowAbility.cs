using UnityEngine;

public class ThrowAbility : MonoBehaviour
{
    // References
    public HookResponderVariable _HookResponder;

    // public Vector2Reference _TargetDirection;
    // public Vector2Reference _TargetPosition;
    public Vector2ComplexReference _Aim;

    public FloatReference _FlyTime;
    public FloatReference _FlyHeight;

    public Transform _PickUpAttachTransform;

    // Variables

    // Events
    public GameEvent _OnPickUp;
    public GameEvent _OnThrow;

    // Mono Behaviour

    // Methods
    public bool IsAiming()
    { return (!_Aim.Value.Direction.Equals(Vector2.zero)); }

    public void PickUp()
    {
        _OnPickUp.Raise();
        _HookResponder.Value.AttachToObject(_PickUpAttachTransform);
    }

    public void Throw()
    {
        if (!IsAiming()) { return; }

        _OnThrow.Raise();
        _HookResponder.Value.StartThrow(_Aim.Value.EndPoint, _FlyTime.Value, _FlyHeight.Value);
        _HookResponder.Value = null;
    }
}
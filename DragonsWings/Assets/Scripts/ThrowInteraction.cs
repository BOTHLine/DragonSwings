using UnityEngine;

// TODO: Rename "ThrowResponder", "HookResponder", "HurtBoxResponder" etc.
public class ThrowInteraction : MonoBehaviour
{
    public bool _AutoAim;
    public int _AutoAimPriority;

    public BoolReference _IsDamagableByThrow;
    public HurtBox _HurtBox;

    public GameEvent _OnHitByThrowable;
    public ThrowableUnityEvent _OnHitByThrowableUnityEvent;

    public void HitByThrow(Throwable throwable)
    {
        _OnHitByThrowable.Raise();
        _OnHitByThrowableUnityEvent.Invoke(throwable);
    }
}
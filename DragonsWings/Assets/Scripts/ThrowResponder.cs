using UnityEngine;

// TODO: Rename "ThrowResponder", "HookResponder", "HurtBoxResponder" etc.
public class ThrowResponder : MonoBehaviour
{
    // Variables
    public bool _AutoAim;
    public int _AutoAimPriority;

    public BoolReference _IsDamagableByThrow;
    public HurtBox _HurtBox;

    // Events
    public UnityEngine.Events.UnityEvent OnHitByThrowUnityEvent;

    public void HitByThrow(HookResponder hookResponder)
    {
        OnHitByThrowUnityEvent.Invoke();
    }
}
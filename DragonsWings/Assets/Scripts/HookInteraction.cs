using UnityEngine;

public class HookInteraction : MonoBehaviour
{
    public bool _AutoAim;
    public int _AutoAimPriority;

    public Weight _Weight;

    public GameEvent _OnHitByHook;
    public HookUnityEvent _OnHitByHookUnityEvent;

    public void HitByHook(Hook hook)
    {
        _OnHitByHook.Raise();
        _OnHitByHookUnityEvent.Invoke(hook);
    }
}
using UnityEngine;

public class InputDispatcher : MonoBehaviour
{
    public HookResponderVariable _AttachedHookResponder;

    public UnityEngine.Events.UnityEvent OnResolvedHookInput;
    public UnityEngine.Events.UnityEvent OnResolvedThrowInput;

    public void ResolveHookInput()
    {
        if (_AttachedHookResponder.Value == null)
        { OnResolvedHookInput.Invoke(); }

        else
        { OnResolvedThrowInput.Invoke(); }
    }
}
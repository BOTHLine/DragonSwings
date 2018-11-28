using UnityEngine;

public class HookResponder : MonoBehaviour
{
    // Components
    public Rigidbody2D _Rigidbody2D;
    public PushBox _PushBox;

    // References
    public bool _AutoAim;
    public int _AutoAimPriority;

    // Variables
    public Weight _Weight;

    private Transform _OldParent;

    // Events
    public HookUnityEvent _OnHitByHookUnityEvent;

    // Mono Behaviour
    private void Awake()
    {
        _Rigidbody2D = GetComponentInParent<Rigidbody2D>();
        // TODO GetComponentInSiblings
        _PushBox = transform.parent.GetComponentInChildren<PushBox>();

        _OldParent = transform.parent.parent;
    }

    // Methods
    public void HitByHook(Hook hook)
    {
        _OnHitByHookUnityEvent.Invoke(hook);
    }

    public void AttachToObject(Transform targetObject)
    {
        transform.parent.parent = targetObject.transform;
        _PushBox._Collider2D.enabled = false;
        _Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        transform.parent.transform.position = targetObject.transform.position;
    }

    public void DetachFromObject()
    {
        transform.parent.parent = _OldParent;
        _PushBox._Collider2D.enabled = true;
        _Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PushBox : MonoBehaviour
{
    // Components
    private Collider _Collider;

    public UnityEvent _OnCollide;

    // Mono Behaviour
    private void OnEnable()
    { _Collider.enabled = true; }

    private void OnDisable()
    { _Collider.enabled = false; }

    private void Awake()
    {
        _Collider = GetComponent<Collider>();
        _Collider.isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    { _OnCollide.Invoke(); Debug.Log("Collide"); }

    // Methods
    public void Enable() { enabled = true; }
    public void Disable() { enabled = false; }
}
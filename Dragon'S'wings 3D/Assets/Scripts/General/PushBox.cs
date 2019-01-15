using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PushBox : MonoBehaviour
{
    // Components
    [HideInInspector] public Collider _Collider;

    public UnityEvent _OnCollide;

    // Mono Behaviour
    private void Awake()
    {
        _Collider = GetComponent<Collider>();
        _Collider.isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    { _OnCollide.Invoke(); }
}
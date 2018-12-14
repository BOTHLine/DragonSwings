using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PushBox : MonoBehaviour
{
    // Components
    [HideInInspector] public Collider2D _Collider2D;

    public UnityEvent _OnCollide;

    // Mono Behaviour
    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
        _Collider2D.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { _OnCollide.Invoke(); }
}
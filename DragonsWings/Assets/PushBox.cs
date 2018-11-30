using UnityEngine;

public class PushBox : MonoBehaviour
{
    // Components
    [HideInInspector] public Collider2D _Collider2D;

    public UnityEngine.Events.UnityEvent OnCollide;

    // Mono Behaviour
    private void Awake()
    { _Collider2D = GetComponent<Collider2D>(); }

    private void OnCollisionEnter2D(Collision2D collision)
    { OnCollide.Invoke(); }
}
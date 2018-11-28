using UnityEngine;

public class PushBox : MonoBehaviour
{
    // Components
    public Collider2D _Collider2D;

    // Mono Behaviour
    private void Awake()
    { _Collider2D = GetComponent<Collider2D>(); }
}
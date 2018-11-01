using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    // Components
    private Rigidbody2D Rigidbody2D;

    // Variables
    [SerializeField] private FloatReference _MoveSpeed;

    [SerializeField] private Vector2Reference _MoveDirection;

    // Events
    // Coroutines

    //Methods
    private void Awake() { Rigidbody2D = GetComponentInParent<Rigidbody2D>(); }

    public void Move() { Rigidbody2D.velocity = _MoveDirection.Value * _MoveSpeed; }
}
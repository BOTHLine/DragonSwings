using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AttackBox : MonoBehaviour
{
    public Vector2Reference targetPosition;

    public GameEvent OnPlayerInAttackBox;

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(targetPosition - (Vector2)transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            OnPlayerInAttackBox.Raise();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            OnPlayerInAttackBox.Raise();
    }
}
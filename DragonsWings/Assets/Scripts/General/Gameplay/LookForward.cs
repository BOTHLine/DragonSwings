
using UnityEngine;

public class LookForward : MonoBehaviour
{
    new public Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rigidbody2D.velocity.Equals(Vector2.zero))
            return;

        float zRotation = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(0.0f, 0.0f, zRotation - 90.0f);
    }
}
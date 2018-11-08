using UnityEngine;

public class LookForward : MonoBehaviour
{
    new public Rigidbody2D rigidbody2D;
    public float correctionValue;

    private void Awake()
    {
        rigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rigidbody2D.velocity.Equals(Vector2.zero))
            return;

        transform.parent.rotation = Utils.GetLookAtRotation(rigidbody2D.velocity, correctionValue);
    }
}
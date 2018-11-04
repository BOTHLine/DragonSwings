using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class HurtBox : MonoBehaviour
{
    public FloatReference health;

    public GameEvent OnHurt;
    public GameEvent OnDie;

    public void Hurt(float damage)
    {
        OnHurt.Raise();
        health.Variable.Value -= damage;
        if (health <= 0.0f)
            Die();
    }

    public void Die()
    {
        OnDie.Raise();
    }
}
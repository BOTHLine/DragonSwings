using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class HurtBox : MonoBehaviour
{
    public FloatReference healthMax;
    public FloatSet healthActual;

    public GameEvent OnHurt;
    public GameEvent OnDie;

    private void Awake()
    {
        healthActual.Add(transform, healthMax);
    }

    public void Hurt(float damage)
    {
        OnHurt.Raise();
        healthActual.ChangeValue(transform, -damage);
        if (healthActual.Get(transform) <= 0.0f)
            Die();
    }

    public void Die()
    {
        OnDie.Raise();
    }
}
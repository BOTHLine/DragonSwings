using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class HurtBox : MonoBehaviour
{
    public FloatReference healthMax;
    public FloatReference healthActual;

    public GameEvent OnHurt;
    public GameEvent OnDie;

    private void Awake()
    {
        healthActual.Variable.Value = healthMax;
    }

    public void Hurt(float damage)
    {
        OnHurt.Raise();
        healthActual.Variable.Value -= damage;
        if (healthActual <= 0.0f)
            Die();
    }

    public void Die()
    {
        OnDie.Raise();
    }
}
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HurtBox : MonoBehaviour
{
    public FloatReference _HealthMax;
    public FloatReference _HealthActual;

    public bool _CanBeHooked = true;
    public bool _CanBeThrownAt = true;

    public GameEvent _OnHurt;
    public UnityEngine.Events.UnityEvent _OnDieEvent;

    private void Awake()
    {
        _HealthActual.Value = _HealthMax;
    }

    public void Hurt(float damage)
    {
        _OnHurt.Raise();
        _HealthActual.Value -= damage;
        if (_HealthActual <= 0.0f)
            Die();
    }

    public void Die()
    { _OnDieEvent.Invoke(); }
}
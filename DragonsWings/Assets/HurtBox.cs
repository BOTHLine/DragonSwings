using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HurtBox : MonoBehaviour
{
    public FloatReference _HealthMax;
    public FloatReference _HealthActual;

    public GameEvent _OnHurt;
    public UnityEngine.Events.UnityEvent _OnDieEvent;

    private void Awake()
    { _HealthActual.Value = _HealthMax; }

    public void Hurt(float damage)
    {
        _OnHurt.Raise();
        _HealthActual.Value -= damage;
        if (CheckDead())
            Die();
    }

    public void HitByHook(Hook hook)
    {

    }

    public void HitByThrowable(Throwable throwable)
    {

    }

    public bool CheckDead()
    { return _HealthActual <= 0.0f; }

    public void Die()
    { _OnDieEvent.Invoke(); }
}
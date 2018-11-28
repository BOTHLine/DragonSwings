using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HurtBox : MonoBehaviour
{
    public FloatReference _HealthMax;
    public FloatReference _HealthCurrent;

    public GameEvent _OnHurt;
    public UnityEngine.Events.UnityEvent _OnDieEvent;

    private void Awake()
    { _HealthCurrent.Value = _HealthMax; }

    public void Hurt(float damage)
    {
        _OnHurt.Raise();
        _HealthCurrent.Value -= damage;
        if (CheckDead())
            Die();
    }

    public bool CheckDead()
    { return _HealthCurrent <= 0.0f; }

    public void Die()
    { _OnDieEvent.Invoke(); }
}
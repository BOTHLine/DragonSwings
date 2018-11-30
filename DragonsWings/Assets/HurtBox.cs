using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour
{
    public FloatReference _HealthMax;
    public FloatReference _HealthCurrent;

    // public GameEvent OnHurt;

    public UnityEngine.Events.UnityEvent OnHurtEvent;
    public UnityEngine.Events.UnityEvent OnDieEvent;

    private void Awake()
    { _HealthCurrent.Value = _HealthMax; }

    public void Hurt(float damage)
    {
        // OnHurt.Raise();
        OnHurtEvent.Invoke();
        _HealthCurrent.Value -= damage;
        if (CheckDead())
            Die();
    }

    public bool CheckDead()
    { return _HealthCurrent <= 0.0f; }

    public void Die()
    { OnDieEvent.Invoke(); }
}
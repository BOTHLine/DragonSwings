using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour
{
    [HideInInspector] public Collider2D _Collider2D;

    //References
    public FloatReference _HealthMax;
    public FloatReference _HealthCurrent;

    // public GameEvent OnHurt;

    //Events
    public UnityEvent _OnHurtEvents;
    public UnityEvent _OnDieEvents;

    // Mono Behaviour
    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
        _HealthCurrent.Value = _HealthMax.Value;
    }

    // Methods
    public void Hurt(float damage)
    {
        if (gameObject.activeInHierarchy)
        {
            _OnHurtEvents.Invoke();
            _HealthCurrent.Value -= damage;
            if (CheckDead())
                Die();
        }
    }

    public bool CheckDead()
    { return _HealthCurrent.Value <= 0.0f; }

    public void Die()
    { _OnDieEvents.Invoke(); }
}
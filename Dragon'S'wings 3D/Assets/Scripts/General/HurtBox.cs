using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class HurtBox : MonoBehaviour
{
    [HideInInspector] public Collider _Collider;

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
        _Collider = GetComponent<Collider>();
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
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour
{
    [HideInInspector] public Collider2D _Collider2D;

    //References
    public FloatReference _HealthMax;
    public FloatReference _HealthCurrent;

    // public GameEvent OnHurt;

    //Events
    public UnityEngine.Events.UnityEvent OnHurtEvent;
    public UnityEngine.Events.UnityEvent OnDieEvent;

    // Mono Behaviour
    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
        _HealthCurrent.Value = _HealthMax;
    }

    // Methods
    public void Hurt(float damage)
    {
        if (gameObject.activeInHierarchy)
        {
            OnHurtEvent.Invoke();
            _HealthCurrent.Value -= damage;
            if (CheckDead())
                Die();
        }
    }

    public bool CheckDead()
    { return _HealthCurrent <= 0.0f; }

    public void Die()
    { OnDieEvent.Invoke(); }
}
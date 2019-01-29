using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class HurtBox : MonoBehaviour
{
    private Collider _Collider;

    //References
    public FloatReference _HealthMax;
    public FloatReference _HealthCurrent;

    // public GameEvent OnHurt;

    //Events
    public UnityEvent _OnHurtEvents;
    public UnityEvent _OnDieEvents;

    // Mono Behaviour
    private void OnEnable()
    { _Collider.enabled = true; }

    private void OnDisable()
    { _Collider.enabled = false; }

    private void Awake()
    {
        _Collider = GetComponent<Collider>();
        _Collider.isTrigger = true;
        _HealthCurrent.Value = _HealthMax.Value;
    }

    // Methods
    public void Enable() { enabled = true; }
    public void Disable() { enabled = false; }

    public void Hurt(float damage)
    {
        if (gameObject.activeInHierarchy)
        {
            _OnHurtEvents.Invoke();
            Debug.Log("Health before: " + _HealthCurrent.Value);
            _HealthCurrent.Value -= damage;
            Debug.Log("Health after: " + _HealthCurrent.Value);
            if (CheckDead())
                Die();
        }
    }

    public bool CheckDead()
    { return _HealthCurrent.Value <= 0.0f; }

    public void Die()
    { _OnDieEvents.Invoke(); }
}
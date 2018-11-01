using UnityEngine;

public class DashBehaviour : MonoBehaviour
{
    // Components
    private Rigidbody2D _Rigidbody2D;

    // Variables
    [SerializeField] private FloatReference _DashSpeed;
    [SerializeField] private FloatReference _DashRange;

    [SerializeField] private Vector2Reference _Position;
    [SerializeField] private Vector2Reference _TargetPosition;

    [SerializeField] private BoolReference _CanDash;
    [SerializeField] private BoolReference _IsDashing;

    // Events
    public GameEvent OnDashStart;
    public GameEvent OnDashEnd;

    // Coroutines
    private System.Collections.IEnumerator _DashRoutine;

    // Methods
    private void Awake()
    {
        _Rigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    public void Dash()
    {
        _DashRoutine = DashRoutine();
        StartCoroutine(_DashRoutine);
    }

    private System.Collections.IEnumerator DashRoutine()
    {
        _IsDashing.Variable.Value = true;
        _CanDash.Variable.Value = false;
        Vector2 start = _Position;
        Vector2 direction = (_TargetPosition - _Position).normalized;
        Vector2 destination = _Position + direction * _DashRange;

        OnDashStart.Raise();

        do
        {
            _Rigidbody2D.velocity = direction * _DashSpeed;
            yield return new WaitForFixedUpdate();
        } while ((destination - start).sqrMagnitude >= (_Position - start).sqrMagnitude);
        _Rigidbody2D.velocity = Vector2.zero;

        _IsDashing.Variable.Value = false;
        OnDashEnd.Raise();
    }
}
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody))]
public class StateController : MonoBehaviour
{
    // Components
    [HideInInspector] public Rigidbody _Rigidbody;
    [HideInInspector] public Animator _Animator;
    [HideInInspector] public SpriteRenderer _SpriteRenderer;

    // Variables
    public Statemachine _Statemachine;
    public State _CurrentState { get; private set; }

    // Methods
    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();

        _Animator = transform.Find("Renderer").GetComponent<Animator>();
        _SpriteRenderer = transform.Find("Renderer").GetComponent<SpriteRenderer>();
    }

    private void Start()
    { _CurrentState = _Statemachine.Initialize(this); }

    private void Update()
    { _Statemachine.UpdateStatemachine(this, _CurrentState); }

    public bool TransitionToState(State nextState)
    {
        if (nextState == null)
            return false;

        _Statemachine.EnterState(this, _CurrentState, nextState);
        _CurrentState = nextState;
        return true;
    }

    public void Respawn()
    {
        // TODO Set Position to LastSavePosition Vector3Reference;
    }
}
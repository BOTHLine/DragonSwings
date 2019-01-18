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
    public Statemachine statemachine;
    public State currentState;

    // Methods
    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();

        _Animator = transform.Find("Renderer").GetComponent<Animator>();
        _SpriteRenderer = transform.Find("Renderer").GetComponent<SpriteRenderer>();
    }

    private void Start()
    { statemachine.Initialize(this); }

    private void Update()
    { statemachine.UpdateStatemachine(this, currentState); }

    public bool TransitionToState(State nextState)
    {
        if (nextState == null)
            return false;

        statemachine.EnterState(this, currentState, nextState);
        currentState = nextState;
        return true;
    }

    public void Respawn()
    {
        // TODO Set Position to LastSavePosition Vector3Reference;
    }
}
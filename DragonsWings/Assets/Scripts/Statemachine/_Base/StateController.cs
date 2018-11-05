using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class StateController : MonoBehaviour
{
    // Components
    [HideInInspector] new public Rigidbody2D rigidbody2D;
    [HideInInspector] public CircleCollider2D circleCollider2D;
    [HideInInspector] public Animator animator;

    // Variables
    public State currentState;
    public State remainState;

    [HideInInspector] public bool canDash = true;

    // Methods
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public bool TransitionToState(State nextState)
    {
        if (nextState == remainState)
            return false;

        currentState.ExitState(this);
        currentState = nextState;
        currentState.EnterState(this);
        return true;
    }

    public void Respawn()
    {
        // TODO Set Position to LastSavePosition Vector2Reference;
    }
}
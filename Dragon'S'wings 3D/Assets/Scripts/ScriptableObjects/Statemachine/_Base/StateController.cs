using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
public class StateController : MonoBehaviour
{
    // Components
    [HideInInspector] public new Rigidbody2D rigidbody2D;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    // Variables
    public Statemachine statemachine;
    public State currentState;

    // Methods
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        animator = transform.Find("Renderer").GetComponent<Animator>();
        spriteRenderer = transform.Find("Renderer").GetComponent<SpriteRenderer>();
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
        // TODO Set Position to LastSavePosition Vector2Reference;
    }
}
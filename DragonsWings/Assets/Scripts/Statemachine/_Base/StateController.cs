﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class StateController : MonoBehaviour
{
    // Components
    [HideInInspector] public new Rigidbody2D rigidbody2D;
    [HideInInspector] public CircleCollider2D circleCollider2D;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Hook hook;

    // Variables
    public State currentState;
    public State remainState;

    // Methods
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        animator = transform.Find("Renderer").GetComponent<Animator>();
        spriteRenderer = transform.Find("Renderer").GetComponent<SpriteRenderer>();

        hook = GetComponentInChildren<Hook>();
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
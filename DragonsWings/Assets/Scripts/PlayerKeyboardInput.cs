using UnityEngine;

public class PlayerKeyboardInput : MonoBehaviour
{
    // References
    public Transform _Player;

    // Variables
    // public Vector2Reference _MoveDirection;
    // public Vector2Reference _AimDirection;

    public Vector2ComplexReference _MoveInput;
    public Vector2ComplexReference _AimInput;

    // Events
    public GameEvent _OnInputActionButton;
    public GameEvent _OnInputAimAutoToggle;

    // Mono Behaviour
    private void Update()
    {
        // _MoveDirection.Value = GetMoveDirection();
        // _AimDirection.Value = GetAimDirection();

        Vector2Complex moveInput = new Vector2Complex(_Player.transform.position, _Player.transform.position);
        moveInput.Direction = GetMoveDirection();
        _MoveInput.Value = moveInput;

        _AimInput.Value = new Vector2Complex(_Player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetKeyDown(KeyCode.Mouse0))
            _OnInputActionButton.Raise();

        if (Input.GetKeyDown(KeyCode.O))
            _OnInputAimAutoToggle.Raise();
    }

    // Methods
    private Vector2 GetMoveVector()
    {
        Vector2 moveVector = new Vector2();
        if (Input.GetKey(KeyCode.W))
            moveVector.y += 1.0f;
        if (Input.GetKey(KeyCode.D))
            moveVector.x += 1.0f;
        if (Input.GetKey(KeyCode.S))
            moveVector.y -= 1.0f;
        if (Input.GetKey(KeyCode.A))
            moveVector.x -= 1.0f;

        return moveVector.normalized;
    }

    private Vector2 GetMoveDirection()
    {
        return GetMoveVector().normalized;
    }

    private Vector2 GetAimDirection()
    {
        Vector2 targetVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _Player.position;
        if (targetVector.magnitude > 1.0f)
            targetVector = targetVector.normalized;
        return targetVector;
    }
}
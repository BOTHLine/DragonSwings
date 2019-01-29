using UnityEngine;

public class PlayerKeyboardInput : MonoBehaviour
{
    // References
    public Transform _Player;
    private Camera _CameraMain;

    // Variables
    // public Vector2Reference _MoveDirection;
    // public Vector2Reference _AimDirection;

    public Vector3ComplexReference _MoveInput;
    public Vector3ComplexReference _AimInput;

    // Events
    public GameEvent _OnInputActionButton;
    public GameEvent _OnInputAimToggle;

    private void Awake()
    {
        _CameraMain = Camera.main;
    }

    // Mono Behaviour
    private void Update()
    {
        // _MoveDirection.Value = GetMoveDirection();
        // _AimDirection.Value = GetAimDirection();

        Vector3Complex moveInput = new Vector3Complex(_Player.transform.position, _Player.transform.position);
        moveInput.Direction = GetMoveDirection();
        _MoveInput.Value = moveInput;

        _AimInput.Value = new Vector3Complex(_Player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetKeyDown(KeyCode.Mouse0))
            _OnInputActionButton.Raise();

        if (Input.GetKeyDown(KeyCode.O))
            _OnInputAimToggle.Raise();
    }

    // Methods
    private Vector3 GetMoveVector()
    {
        Vector3 moveVector = new Vector3();
        if (Input.GetKey(KeyCode.W))
            moveVector.z += 1.0f;
        if (Input.GetKey(KeyCode.D))
            moveVector.x += 1.0f;
        if (Input.GetKey(KeyCode.S))
            moveVector.z -= 1.0f;
        if (Input.GetKey(KeyCode.A))
            moveVector.x -= 1.0f;

        return moveVector.normalized;
    }

    private Vector3 GetMoveDirection()
    {
        return GetMoveVector().normalized;
    }

    private Vector3 GetAimDirection()
    {
        Vector3 targetVector = _CameraMain.ScreenToWorldPoint(Input.mousePosition) - _Player.position;
        if (targetVector.magnitude > 1.0f)
            targetVector = targetVector.normalized;
        return targetVector;
    }
}
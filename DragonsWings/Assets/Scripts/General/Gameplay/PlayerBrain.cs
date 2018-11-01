using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBrain : MonoBehaviour
{
    // Components

    // Variables
    [SerializeField] private Vector2Reference _PlayerPosition;
    [SerializeField] private Vector2Reference _MoveDirection;
    [SerializeField] private Vector2Reference _AimPosition;

    [SerializeField] private EntityState _CurrentPlayerState;
    [SerializeField] private HookState _CurrentHookState;

    // Events
    [SerializeField] private GameEvent _OnMoveInput;
    [SerializeField] private GameEvent _OnDashInput;
    [SerializeField] private GameEvent _OnHookInput;
    [SerializeField] private GameEvent _OnPullInput;
    [SerializeField] private GameEvent _OnSwingInput;
    [SerializeField] private GameEvent _OnReleaseInput;

    // Coroutines

    // Methods
    private void Update()
    {
        UpdatePlayerPosition();
        CheckAimInput();

        switch (_CurrentPlayerState)
        {
            case EntityState.Movement:
                CheckMovementInput();
                switch (_CurrentHookState)
                {
                    case HookState.Free:
                        CheckDashInput();
                        CheckHookInput();
                        break;
                    case HookState.Flying:
                        CheckDashInput();
                        break;
                    case HookState.Clipped:
                        CheckReleaseInput();
                        CheckSwingInput();
                        break;
                }
                break;
            case EntityState.Dash:
                break;
            case EntityState.Fall:
                switch (_CurrentHookState)
                {
                    case HookState.Free:
                        CheckHookInput();
                        CheckDashInput();
                        break;
                    case HookState.Flying:
                        CheckDashInput();
                        break;
                    case HookState.Clipped:
                        CheckSwingInput();
                        break;
                }
                break;
            case EntityState.Pull:
                CheckReleaseInput();
                break;
            case EntityState.Swing:
                CheckReleaseInput();
                break;
            case EntityState.Pushed:
                break;
        }
    }

    private void UpdatePlayerPosition() { _PlayerPosition.Variable.Value = transform.position; }

    private void CheckAimInput() { _AimPosition.Variable.Value = Camera.main.ScreenToWorldPoint(Input.mousePosition); }
    private void CheckMovementInput() { _MoveDirection.Variable.Value = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; _OnMoveInput.Raise(); }
    private void CheckDashInput() { if (Input.GetKeyDown(KeyCode.Space)) _OnDashInput.Raise(); }
    private void CheckHookInput() { if (Input.GetKeyDown(KeyCode.Mouse0)) _OnHookInput.Raise(); }
    private void CheckReleaseInput() { if (Input.GetKeyDown(KeyCode.Mouse0)) _OnReleaseInput.Raise(); }
    private void CheckPullInput() { if (Input.GetKeyDown(KeyCode.LeftShift)) _OnPullInput.Raise(); }
    private void CheckSwingInput() { if (Input.GetKeyDown(KeyCode.Space)) _OnSwingInput.Raise(); }

    public void ChangeEntityStateToMovement() { _CurrentPlayerState = EntityState.Movement; }
    public void ChangeEntityStateToDash() { _CurrentPlayerState = EntityState.Dash; }
    public void ChangeEntityStateToFall() { _CurrentPlayerState = EntityState.Fall; }
    public void ChangeEntityStateToPull() { _CurrentPlayerState = EntityState.Pull; }
    public void ChangeEntityStateToSwing() { _CurrentPlayerState = EntityState.Swing; }
    public void ChangeEntityStateToPushed() { _CurrentPlayerState = EntityState.Pushed; }
    public void ChangeHookStateToFree() { _CurrentHookState = HookState.Free; }
    public void ChangeHookStateToFlying() { _CurrentHookState = HookState.Flying; }
    public void ChangeHookStateToClipped() { _CurrentHookState = HookState.Clipped; }
}

public enum EntityState
{
    Movement,
    Dash,
    Fall,
    Pull,
    Swing,
    Pushed
}

public enum HookState
{
    Free,
    Flying,
    Clipped
}
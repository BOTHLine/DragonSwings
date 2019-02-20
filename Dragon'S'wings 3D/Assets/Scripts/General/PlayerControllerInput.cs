using UnityEngine;

public class PlayerControllerInput : MonoBehaviour
{
    // Components
    public Transform _Player;

    // Variables
    public FloatReference _DeadValue;

    // public Vector2Reference _MoveDirection;
    // public Vector2Reference _AimDirection;

    public Vector3ComplexReference _MoveInput;
    public Vector3ComplexReference _AimInput;

    public FloatReference _AimVelocity;
    private System.Collections.Generic.List<Vector2> _LastAimPositions;
    private int _AmountOfPositionsForVelocity = 5;

    private System.Collections.Generic.Dictionary<string, bool> _IsAxisInUse;

    // Events
    public GameEvent _OnInputAction;
    public GameEvent _OnInputAimAutoToggle;
    public GameEvent _OnInputControlsShowToggle;

    // Mono Behaviour
    private void Awake()
    {
        _LastAimPositions = new System.Collections.Generic.List<Vector2>();
        _IsAxisInUse = new System.Collections.Generic.Dictionary<string, bool>();
    }

    private void Update()
    {
        // TODO:
        // Change to Vector2ComplexReference System in Dotted Line (Rewrite anyway), HookAutoAim, ThrowAutoAim (merge them?), HookAbility, ThrowAbility and CameraFollow.

        ProcessMoveInput();
        ProcessAimInput();
        ProcessAimVelocity();

        //_MoveDirection.Value = GetAxis2D("AxisX", "AxisY");
        //_AimDirection.Value = GetAxis2D("Axis4", "Axis5");

        if (GetAxisDown("Axis10")) { _OnInputAction.Raise(); }

        if (Input.GetKeyDown(KeyCode.JoystickButton4)) { _OnInputAimAutoToggle.Raise(); }
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) { _OnInputControlsShowToggle.Raise(); }
    }

    private void ProcessMoveInput()
    {
        Vector3Complex moveInput = new Vector3Complex(_Player.position, Vector3.zero);
        Vector2 moveInput2D = GetAxis2D("AxisX", "AxisY");
        moveInput.Vector = new Vector3(moveInput2D.x, 0.0f, moveInput2D.y);
        _MoveInput.Value = moveInput;
    }

    private void ProcessAimInput()
    {
        Vector3Complex aimInput = new Vector3Complex(_Player.position, Vector3.zero);
        Vector2 aimInput2D = GetAxis2D("Axis4", "Axis5");
        aimInput.Vector = new Vector3(aimInput2D.x, 0.0f, aimInput2D.y);
        _AimInput.Value = aimInput;
    }

    private void ProcessAimVelocity()
    {
        _LastAimPositions.Add(_AimInput.Value.Vector);
        if (_LastAimPositions.Count >= _AmountOfPositionsForVelocity)
        {
            _LastAimPositions.RemoveAt(0);
        }
        _AimVelocity.Value = Vector2.Distance(_AimInput.Value.Vector, _LastAimPositions[0]);
    }

    // Methods
    private float GetAxisRaw(string name)
    { return Input.GetAxisRaw(name); }

    private float GetAxis(string name, float dead)
    {
        float value = GetAxisRaw(name);
        if (Mathf.Abs(value) < dead)
        {
            _IsAxisInUse.Remove(name);
            return 0.0f;
        }
        else
        {
            _IsAxisInUse[name] = true;
            return value;
        }
    }

    private float GetAxis(string name)
    { return GetAxis(name, _DeadValue.Value); }

    private Vector2 GetAxis2D(string nameX, string nameY, float dead)
    {
        Vector2 axis2D = new Vector2(GetAxisRaw(nameX), -GetAxisRaw(nameY));
        float magnitudeFactor = axis2D.magnitude;
        if (magnitudeFactor < Mathf.Sqrt(dead * dead + dead * dead))
            return Vector2.zero;
        else if (magnitudeFactor > 1)
            return new Vector2(axis2D.x / magnitudeFactor, axis2D.y / magnitudeFactor);
        return axis2D;
    }

    private Vector2 GetAxis2D(string nameX, string nameY)
    { return GetAxis2D(nameX, nameY, _DeadValue.Value); }

    private bool GetAxisDown(string name, float dead)
    {
        bool value;
        value = _IsAxisInUse.TryGetValue(name, out value);

        if (Mathf.Abs(Input.GetAxisRaw(name)) >= dead)
        {
            _IsAxisInUse[name] = true;
            return (!value);
        }
        else
        {
            _IsAxisInUse.Remove(name);
            return false;
        }
    }

    private bool GetAxisDown(string name)
    { return GetAxisDown(name, _DeadValue.Value); }
}
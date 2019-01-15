using UnityEngine;

public class PlayerControllerInput : MonoBehaviour
{
    // Components
    public Transform _Player;

    // Variables
    public FloatReference _DeadValue;

    // public Vector2Reference _MoveDirection;
    // public Vector2Reference _AimDirection;

    public Vector2ComplexReference _MoveInput;
    public Vector2ComplexReference _AimInput;

    private System.Collections.Generic.Dictionary<string, bool> _IsAxisInUse;

    // Events
    public GameEvent _OnInputAction;
    public GameEvent _OnInputAimAutoToggle;
    public GameEvent _OnInputControlsShowToggle;

    // Mono Behaviour
    private void Awake()
    { _IsAxisInUse = new System.Collections.Generic.Dictionary<string, bool>(); }

    private void Update()
    {
        // TODO:
        // Change to Vector2ComplexReference System in Dotted Line (Rewrite anyway), HookAutoAim, ThrowAutoAim (merge them?), HookAbility, ThrowAbility and CameraFollow.
        Vector2Complex moveInput = new Vector2Complex(_Player.position, _Player.position);
        moveInput.Direction = GetAxis2D("AxisX", "AxisY");
        _MoveInput.Value = moveInput;

        Vector2Complex aimInput = new Vector2Complex(_Player.position, _Player.position);
        aimInput.Direction = GetAxis2D("Axis4", "Axis5");
        _AimInput.Value = aimInput;

        //_MoveDirection.Value = GetAxis2D("AxisX", "AxisY");
        //_AimDirection.Value = GetAxis2D("Axis4", "Axis5");

        if (GetAxisDown("Axis10")) { _OnInputAction.Raise(); }

        if (Input.GetKeyDown(KeyCode.JoystickButton4)) { _OnInputAimAutoToggle.Raise(); }
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) { _OnInputControlsShowToggle.Raise(); }
    }

    // Methods
    private float GetAxisRaw(string name)
    {
        return Input.GetAxisRaw(name);
    }

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
    {
        return GetAxis(name, _DeadValue.Value);
    }

    private Vector2 GetAxis2D(string nameX, string nameY, float dead)
    {
        Vector2 Axis2D = new Vector2(GetAxisRaw(nameX), -GetAxisRaw(nameY));
        float magnitudeFactor = Axis2D.magnitude;
        if (magnitudeFactor < Mathf.Sqrt(dead * dead + dead * dead))
            return Vector2.zero;
        else if (magnitudeFactor > 1)
            return new Vector2(Axis2D.x / magnitudeFactor, Axis2D.y / magnitudeFactor);
        return Axis2D;
    }

    private Vector2 GetAxis2D(string nameX, string nameY)
    {
        return GetAxis2D(nameX, nameY, _DeadValue.Value);
    }

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
        }
        return false;
    }

    private bool GetAxisDown(string name)
    {
        return GetAxisDown(name, _DeadValue.Value);
    }
}
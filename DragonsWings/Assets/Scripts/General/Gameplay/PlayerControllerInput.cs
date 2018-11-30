using UnityEngine;

public class PlayerControllerInput : MonoBehaviour
{
    // Variables
    public FloatReference deadValue;

    public Vector2Reference moveDirection;
    public Vector2Reference aimDirection;

    private System.Collections.Generic.Dictionary<string, bool> isAxisInUse;

    // Events
    public GameEvent OnHookInput;
    public GameEvent OnResetHookInput;
    public GameEvent OnResetLevelInput;

    // Mono Behaviour
    private void Awake()
    {
        isAxisInUse = new System.Collections.Generic.Dictionary<string, bool>();
    }

    private void Update()
    {
        moveDirection.Variable.Value = GetAxis2D("AxisX", "AxisY");
        aimDirection.Variable.Value = GetAxis2D("Axis4", "Axis5");

        if (GetAxisDown("Axis10")) { OnHookInput.Raise(); }

        if (Input.GetKeyDown(KeyCode.K)) { OnResetHookInput.Raise(); }

        if (Input.GetKeyDown(KeyCode.L)) { OnResetLevelInput.Raise(); }
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
            isAxisInUse.Remove(name);
            return 0.0f;
        }
        else
        {
            isAxisInUse[name] = true;
            return value;
        }
    }

    private float GetAxis(string name)
    {
        return GetAxis(name, deadValue);
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
        return GetAxis2D(nameX, nameY, deadValue);
    }

    private bool GetAxisDown(string name, float dead)
    {
        bool value;
        value = isAxisInUse.TryGetValue(name, out value);

        if (Mathf.Abs(Input.GetAxisRaw(name)) >= dead)
        {
            isAxisInUse[name] = true;
            return (!value);
        }
        else
        {
            isAxisInUse.Remove(name);
        }
        return false;
    }

    private bool GetAxisDown(string name)
    {
        return GetAxisDown(name, deadValue);
    }

    public void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
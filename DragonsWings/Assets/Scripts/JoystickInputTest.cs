using UnityEngine;

public class JoystickInputTest : MonoBehaviour
{
    public float dead = 0.125f;
    private string[] buttons;

    private void Start()
    {
        buttons = new string[17];
        buttons[0] = "AxisX";
        buttons[1] = "AxisY";
        for (int i = 3; i < 8; i++)
        {
            buttons[i - 1] = "Axis" + i;
        }
        for (int i = 0; i < 10; i++)
        {
            buttons[i + 7] = "Button" + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 7; i++)
        {
            float input = Input.GetAxisRaw(buttons[i]);
            if (Mathf.Abs(input) >= dead)
            {
                Debug.Log(buttons[i] + ": " + input);
            }
        }
        for (int i = 7; i < 17; i++)
        {
            if (Input.GetButtonDown(buttons[i]))
            {
                Debug.Log(buttons[i] + " has been pressed!");
            }
        }
    }
}
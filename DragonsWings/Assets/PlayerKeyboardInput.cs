using UnityEngine;

public class PlayerKeyboardInput : MonoBehaviour
{
    public Vector2Reference playerPosition;

    public Vector2Reference moveDirection;
    public Vector2Reference aimDirection;

    public GameEvent OnHookInput;

    private void Update()
    {
        moveDirection.Variable.Value = GetMoveDirection();
        aimDirection.Variable.Value = GetAimDirection();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnHookInput.Raise();
    }

    private Vector2 GetMoveDirection()
    {
        Vector2 tempMoveDirection = new Vector2();
        if (Input.GetKey(KeyCode.W))
            tempMoveDirection.y += 1.0f;
        if (Input.GetKey(KeyCode.D))
            tempMoveDirection.x += 1.0f;
        if (Input.GetKey(KeyCode.S))
            tempMoveDirection.y -= 1.0f;
        if (Input.GetKey(KeyCode.A))
            tempMoveDirection.x -= 1.0f;

        return tempMoveDirection.normalized;
    }

    private Vector2 GetAimDirection()
    {
        Vector2 targetVector = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPosition;
        if (targetVector.magnitude > 1.0f)
            targetVector = targetVector.normalized;
        return targetVector;
    }
}
using UnityEngine;

public class Move : MonoBehaviour
{
    public float _Speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        { direction += Vector2.up; }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        { direction += Vector2.right; }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        { direction += Vector2.down; }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        { direction += Vector2.left; }

        transform.position = transform.position + (Vector3)direction * Time.deltaTime * _Speed;
    }
}
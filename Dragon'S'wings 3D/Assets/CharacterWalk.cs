using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalk : MonoBehaviour
{

    public float walkSpeed;
    private float currentSpeed;
    public float walkToSprintTime;
    public float sprintStopThreshHold;
    public float sprintSpeed;
    public bool walkingIsAllowed = true;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (walkToSprintTime + timer < Time.time)
        { if (currentSpeed <= sprintSpeed) currentSpeed += 0.001f; }

        else
        { currentSpeed = walkSpeed; }

        if (walkingIsAllowed)
            transform.position = new Vector3(transform.position.x + Input.GetAxis("WalkHorizontal") * currentSpeed, transform.position.y, transform.position.z + Input.GetAxis("WalkVertical") * currentSpeed);

        if (Input.GetAxis("WalkHorizontal") <= sprintStopThreshHold && Input.GetAxis("WalkHorizontal") >= -sprintStopThreshHold &&
             Input.GetAxis("WalkVertical") <= sprintStopThreshHold && Input.GetAxis("WalkVertical") >= -sprintStopThreshHold)
        {
            timer = Time.time;
        }
    }
}
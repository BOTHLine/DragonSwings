using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    private Vector3 startposition;
	// Use this for initialization
	void Start ()
    {
        startposition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y < -20) transform.position = startposition;
	}
}

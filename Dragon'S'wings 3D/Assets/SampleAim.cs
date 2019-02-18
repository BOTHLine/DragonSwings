using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAim : MonoBehaviour
{
    public Transform _AimTarget;

    public float _MaxRange;
    public float _LengthSpeed;
    public float _AngleSpeed;

    private LineRenderer _LineRenderer;

    private void Awake()
    {
        _LineRenderer = GetComponent<LineRenderer>();
        _LineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentAim = _AimTarget.position - transform.position;
        Vector3 newAim = new Vector3(Input.GetAxisRaw("AxisX"), 0.0f, -Input.GetAxisRaw("AxisY"));

        Vector3 currentAimDirection = currentAim.normalized;
        Vector3 newAimDirection = newAim.normalized;

        float currentAimLength = currentAim.magnitude;
        float newAimLength = Mathf.Clamp01(newAim.magnitude) * _MaxRange;
        Debug.Log("Current Aim Length: " + currentAimLength);
        Debug.Log("New Aim Length: " + newAimLength);

        float calculatedAimLength = Mathf.Lerp(currentAimLength, newAimLength, _LengthSpeed * Time.deltaTime);
        Debug.Log("Calculated Aim Length: " + calculatedAimLength);
        Vector3 calculatedAimDirection = Vector3.Slerp(currentAimDirection, newAimDirection, _AngleSpeed * Time.deltaTime);
        Vector3 calculatedAim = calculatedAimDirection.normalized * calculatedAimLength;

        _AimTarget.position = transform.position + calculatedAim;

        Vector3[] linePositions = new Vector3[] { transform.position, _AimTarget.position };
        _LineRenderer.SetPositions(linePositions);
    }
}
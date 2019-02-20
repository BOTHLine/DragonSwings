using UnityEngine;

public class AimProcessorLerp : MonoBehaviour
{
    public Vector3ComplexReference _AimVectorIn;
    public Vector3ComplexReference _AimVectorOut;

    public FloatReference _AimSpeedDistance;
    public FloatReference _AimSpeedAngle;

    public FloatReference _MaxAngularMovement;
    public FloatReference _AimSpeedDirect;

    public LayerMask _RaycastLayerMask;

    private void Awake()
    { _AimVectorIn.Subscribe(ProcessAimLerp); }

    public void ProcessAimLerp(Vector3Complex vector3Complex)
    {
        Vector3 targetVectorDirection = _AimVectorIn.Value.Direction;
        float targetVectorMagnitude = _AimVectorIn.Value.Magnitude;

        Vector3 currentVectorDirection = _AimVectorOut.Value.Direction;
        float currentVectorMagnitude = _AimVectorOut.Value.Magnitude;

        Vector3Complex outVector3Complex = _AimVectorOut.Value;
        if (currentVectorDirection.Equals(Vector3.zero))
        {
            outVector3Complex.Direction = targetVectorDirection;
        }
        else
        {
            float angle = Vector3.SignedAngle(currentVectorDirection, targetVectorDirection, Vector3.up);
            if (angle != 0)
            {
                if (Mathf.Abs(angle) <= _MaxAngularMovement.Value)
                {
                    angle = angle / Mathf.Abs(angle) * Mathf.Min(Time.deltaTime * 180 * _AimSpeedAngle.Value, Mathf.Abs(angle));
                    outVector3Complex.Direction = Quaternion.Euler(0.0f, angle, 0.0f) * currentVectorDirection;
                    outVector3Complex.Magnitude = Mathf.Lerp(currentVectorMagnitude, targetVectorMagnitude, Time.deltaTime * _AimSpeedDistance.Value);
                    Debug.Log("If");

                }
                else
                {
                    outVector3Complex.Vector = Vector3.Lerp(_AimVectorIn.Value.Vector, _AimVectorOut.Value.Vector, Time.deltaTime * _AimSpeedDirect.Value);
                    Debug.Log("Else");
                }
            }
        }
        _AimVectorOut.Value = outVector3Complex;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_AimVectorOut.Value.EndPoint, 0.5f);
    }
}
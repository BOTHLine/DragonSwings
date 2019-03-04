using UnityEngine;

public class AimProcessorLerp : MonoBehaviour
{
    public Vector3ComplexReference _AimVectorIn;
    public Vector3ComplexReference _AimVectorOut;

    // Wie schnell Aim die Distanz ändert
    public FloatReference _AimSpeedDistance;
    // Wie schnell Aim den Winkel ändert
    public FloatReference _AimSpeedAngle;

    // Ab welchem Winkelunterschied direktes Movement bevorzugt wird
    public FloatReference _MaxAngularMovement;
    // Wie schnell Aim sich auf direktem Movement bewegt
    public FloatReference _AimSpeedDirect;

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

                }
                else
                {
                    outVector3Complex.Vector = Vector3.Lerp(_AimVectorIn.Value.Vector, _AimVectorOut.Value.Vector, Time.deltaTime * _AimSpeedDirect.Value);
                }
                if ((outVector3Complex.Vector - _AimVectorIn.Value.Vector).magnitude <= 0.01f) outVector3Complex.Vector = _AimVectorIn.Value.Vector;
            }
            else
            {
                if (currentVectorMagnitude != targetVectorMagnitude)
                { outVector3Complex.Magnitude = Mathf.Lerp(currentVectorMagnitude, targetVectorMagnitude, Time.deltaTime * _AimSpeedDistance.Value); }
                if ((outVector3Complex.Vector - _AimVectorIn.Value.Vector).magnitude <= 0.01f) outVector3Complex.Vector = _AimVectorIn.Value.Vector;
            }
        }

        _AimVectorOut.Value = outVector3Complex;
    }
}
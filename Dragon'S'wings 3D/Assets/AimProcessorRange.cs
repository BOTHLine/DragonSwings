using UnityEngine;

public class AimProcessorRange : MonoBehaviour
{
    public Vector3ComplexReference _AimVectorIn;
    public Vector3ComplexReference _AimVectorOut;

    public FloatReference _AimRange;

    private void Awake()
    {
        _AimVectorIn.Subscribe(ProcessAimRange);
    }

    public void ProcessAimRange(Vector3Complex vector3Complex)
    {
        vector3Complex.Magnitude = vector3Complex.Magnitude * _AimRange.Value;
        _AimVectorOut.Value = vector3Complex;
    }
}
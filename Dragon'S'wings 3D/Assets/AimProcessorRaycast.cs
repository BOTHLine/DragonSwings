using UnityEngine;

public class AimProcessorRaycast : MonoBehaviour
{
    public Vector3ComplexReference _AimVectorIn;
    public Vector3ComplexReference _AimVectorOut;

    public LayerMask _RaycastLayerMask;

    private void Update()
    {
        ProcessAimRaycast();
    }

    private void ProcessAimRaycast()
    {
        Vector3Complex outVector3Complex = _AimVectorIn.Value;

        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, _AimVectorOut.Value.Direction, out raycastHit, _AimVectorIn.Value.Magnitude, _RaycastLayerMask))
        { outVector3Complex.Magnitude = raycastHit.distance; }

        _AimVectorOut.Value = outVector3Complex;
    }
}
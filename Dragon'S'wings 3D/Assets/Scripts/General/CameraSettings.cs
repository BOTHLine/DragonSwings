using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public Vector3Reference _CameraTargetPosition;
    public FloatReference _CameraTargetZoom;

    public Vector3Reference _PlayerPosition;
    public Vector3ComplexReference _Aim;

    public FloatReference _CameraZoomMinimum;

    private void Awake()
    {
        _CameraTargetPosition.Value = _PlayerPosition.Value;
        _CameraTargetZoom.Value = _CameraZoomMinimum.Value;
    }

    private void Update()
    { _CameraTargetPosition.Value = (_PlayerPosition + _PlayerPosition + _Aim.Value.EndPoint) / 3.0f; }
}
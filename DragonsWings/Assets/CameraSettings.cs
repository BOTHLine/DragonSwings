using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public Vector2Reference _CameraTargetPosition;
    public FloatReference _CameraTargetZoom;

    public Vector2Reference _PlayerPosition;
    public Vector2Reference _AimPosition;
    public Vector2ComplexReference _Aim;

    public FloatReference _CameraZoomMinimum;

    private void Awake()
    {
        _CameraTargetPosition.Value = _PlayerPosition;
        _CameraTargetZoom.Value = _CameraZoomMinimum;
    }

    private void Update()
    {
        Vector2Complex aim = _Aim.Value;
        // _CameraTargetPosition.Value = (_PlayerPosition + _PlayerPosition + aim.EndPoint) / 3.0f;

        _CameraTargetPosition.Value = (_PlayerPosition + _PlayerPosition + _AimPosition) / 3.0f;
    }
}
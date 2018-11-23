using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public Vector2Reference _CameraTargetPosition;
    public FloatReference _CameraTargetZoom;

    public Vector2Reference _PlayerPosition;
    public Vector2Reference _AimPosition;

    public FloatReference _CameraZoomMinimum;

    private void Awake()
    {
        _CameraTargetPosition.Value = _PlayerPosition;
        _CameraTargetZoom.Value = _CameraZoomMinimum;
    }

    private void Update()
    {
        _CameraTargetPosition.Value = (_PlayerPosition + _PlayerPosition + _AimPosition) / 3.0f;
    }
}
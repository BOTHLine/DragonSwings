using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera _Camera;

    public Vector2Reference _TargetPosition;

    public FloatReference _TargetZoom;

    public FloatReference _CameraMoveSpeed;
    public FloatReference _CameraZoomSpeed;

    private void Awake()
    {
        _Camera = GetComponent<Camera>();

    }

    void LateUpdate()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 cameraFollowPosition = _TargetPosition.Value;

        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector2.Distance(cameraFollowPosition, transform.position);

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * _CameraMoveSpeed.Value * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            { newCameraPosition = cameraFollowPosition; }

            transform.position = newCameraPosition;
        }
    }

    private void HandleZoom()
    {
        float cameraZoomDifference = _TargetZoom.Value - _Camera.orthographicSize;

        _Camera.orthographicSize += cameraZoomDifference * _CameraZoomSpeed.Value * Time.deltaTime;

        if (cameraZoomDifference > 0)
        {
            if (_Camera.orthographicSize > _TargetZoom.Value) { _Camera.orthographicSize = _TargetZoom.Value; }
        }
        else
        {
            if (_Camera.orthographicSize < _TargetZoom.Value) { _Camera.orthographicSize = _TargetZoom.Value; }
        }
    }
}

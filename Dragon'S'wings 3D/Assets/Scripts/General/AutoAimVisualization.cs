using UnityEngine;

public class AutoAimVisualization : MonoBehaviour
{
    public Vector2ComplexReference _AimRaw;
    public Vector2ComplexReference _AimAuto;

    // public Vector2Reference _StartPosition;
    // public Vector2Reference _AimRawDirection;
    // public Vector2Reference _AimRawPosition;
    public FloatReference _AimAutoRadius;
    public FloatReference _AimRange;

    private void OnDrawGizmos()
    {
        // TODO
        /*
        Gizmos.matrix = Matrix4x4.TRS(transform.position, Utils.GetLookAtRotation(_AimRawDirection), Vector3.one);
        Gizmos.DrawCube(_AimRawDirection.Value, new Vector3(_AimRange, _AimAutoRadius, 1.0f));
        */
        if (!Application.isPlaying) return;

        Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.2f);
        for (int i = (int)_AimAutoRadius.Value; i < _AimRange.Value; i++)
        {
            Gizmos.DrawSphere(_AimRaw.Value.StartPoint + _AimRaw.Value.Direction * i, _AimAutoRadius.Value);
        }

        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.25f);
        Gizmos.DrawSphere(_AimRaw.Value.EndPoint, 1.0f);

        Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.25f);
        Gizmos.DrawSphere(_AimAuto.Value.EndPoint, 1.0f);
    }
}
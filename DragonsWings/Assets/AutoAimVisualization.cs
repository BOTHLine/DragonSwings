using UnityEngine;

public class AutoAimVisualization : MonoBehaviour
{
    public Vector2Reference _StartPosition;
    public Vector2Reference _AimRawDirection;
    public Vector2Reference _AimRawPosition;
    public FloatReference _AimAutoRadius;
    public FloatReference _AimRange;

    public Vector2Reference _AimAutoPosition;

    private void OnDrawGizmos()
    {
        // TODO
        /*
        Gizmos.matrix = Matrix4x4.TRS(transform.position, Utils.GetLookAtRotation(_AimRawDirection), Vector3.one);
        Gizmos.DrawCube(_AimRawDirection.Value, new Vector3(_AimRange, _AimAutoRadius, 1.0f));
        */

        Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.2f);
        for (int i = (int)_AimAutoRadius.Value; i < _AimRange; i++)
        {
            Gizmos.DrawSphere(_StartPosition + _AimRawDirection.Value.normalized * i, _AimAutoRadius);
        }

        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.25f);
        Gizmos.DrawSphere(_AimRawPosition.Value, 1.0f);

        Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.25f);
        Gizmos.DrawSphere(_AimAutoPosition.Value, 1.0f);
    }
}
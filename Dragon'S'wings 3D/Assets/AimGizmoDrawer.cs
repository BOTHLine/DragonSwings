using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGizmoDrawer : MonoBehaviour
{
    public Vector3ComplexReference _Vector3Complex;

    public Color color;

    private void OnDrawGizmos()
    {
        if (_Vector3Complex == null) return;

        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position + _Vector3Complex.Value.EndPoint, 0.5f);
    }
}
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AbilityThrowNewArkRenderer : MonoBehaviour
{
    private LineRenderer _LineRenderer;

    private AbilityThrowNew _AbilityThrowNew;

    public FloatReference _ThrowArkThicknessStart;
    public FloatReference _ThrowArkThicknessEnd;

    private void Awake()
    {
        _LineRenderer = GetComponentInChildren<LineRenderer>();
        _LineRenderer.startWidth = _ThrowArkThicknessStart.Value;
        _LineRenderer.endWidth = _ThrowArkThicknessEnd.Value;

        _AbilityThrowNew = GetComponentInParent<AbilityThrowNew>();
    }

    private void Update()
    {
        if (_AbilityThrowNew._HookResponder.Value == null || _AbilityThrowNew._Aim.Value.Direction.Equals(Vector3.zero))
            DeletePath();
        else
            DrawPath();
    }

    private void DrawPath()
    {
        Vector3[] drawPoints = _AbilityThrowNew.CalculateThrowArkPath();
        _LineRenderer.positionCount = drawPoints.Length;
        _LineRenderer.SetPositions(drawPoints);
    }

    private void DeletePath()
    { _LineRenderer.positionCount = 0; }
}

using UnityEngine;

public class ThrowArkRenderer : MonoBehaviour
{
    // Components
    private ThrowAbility _ThrowAbility;

    private LineRenderer _LineRenderer;

    // References
    public FloatReference _ThrowArkHeight;
    public IntReference _ThrowSegmentAmount;
    public FloatReference _ThrowArkThicknessStart;
    public FloatReference _ThrowArkThicknessEnd;

    // Mono Behaviour
    private void Awake()
    {
        _ThrowAbility = GetComponentInParent<ThrowAbility>();
        _LineRenderer = GetComponent<LineRenderer>();

        _LineRenderer.startWidth = _ThrowArkThicknessStart.Value;
        _LineRenderer.endWidth = _ThrowArkThicknessEnd.Value;
    }

    private void Update()
    {
        _LineRenderer.startWidth = _ThrowArkThicknessStart.Value;
        _LineRenderer.endWidth = _ThrowArkThicknessEnd.Value;

        if (!_ThrowAbility.IsAiming())
        { RemoveThrowArk(); }
        else
        { DrawThrowArk(); }

    }

    // Methods
    private void DrawThrowArk()
    {
        _LineRenderer.positionCount = _ThrowSegmentAmount.Value + 1;
        // float steps = ((Vector2)transform.position - _ThrowAbility._Aim.Value.EndPoint).magnitude / _ThrowSegmentAmount.Value;
        // float steps = ((Vector2)transform.position - _ThrowAbility._TargetPosition.Value).magnitude / _ThrowSegmentAmount.Value;

        for (int i = 0; i <= _ThrowSegmentAmount.Value; i++)
        {
            Vector2 nextPoint = Utils.CalculatePositionOnParabola(transform.position, _ThrowAbility._Aim.Value.EndPoint, _ThrowArkHeight.Value, i / (float)_ThrowSegmentAmount.Value);
            // Vector2 nextPoint = Utils.CalculatePositionOnParabola(transform.position, _ThrowAbility._TargetPosition.Value, _ThrowArkHeight.Value, i / (float)_ThrowSegmentAmount.Value);
            _LineRenderer.SetPosition(i, new Vector3(nextPoint.x, nextPoint.y, -1.0f));
        }
    }

    private void RemoveThrowArk()
    { _LineRenderer.positionCount = 0; }
}
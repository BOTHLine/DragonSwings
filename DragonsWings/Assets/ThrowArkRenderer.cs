using UnityEngine;

public class ThrowArkRenderer : MonoBehaviour
{
    // Components
    ThrowAbility _ThrowAbility;

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

        _LineRenderer.startWidth = _ThrowArkThicknessStart;
        _LineRenderer.endWidth = _ThrowArkThicknessEnd;
    }

    private void Update()
    {
        _LineRenderer.startWidth = _ThrowArkThicknessStart;
        _LineRenderer.endWidth = _ThrowArkThicknessEnd;

        if (!_ThrowAbility.IsAiming())
        { RemoveThrowArk(); }
        else
        { DrawThrowArk(); }

    }

    // Methods
    private void DrawThrowArk()
    {
        _LineRenderer.positionCount = _ThrowSegmentAmount.Value + 1;
        float steps = ((Vector2)transform.position - _ThrowAbility._TargetPosition).magnitude / _ThrowSegmentAmount;

        for (int i = 0; i <= _ThrowSegmentAmount; i++)
        {
            Vector2 nextPoint = Utils.CalculatePositionOnParabola(transform.position, _ThrowAbility._TargetPosition, _ThrowArkHeight, i / (float)_ThrowSegmentAmount);
            _LineRenderer.SetPosition(i, new Vector3(nextPoint.x, nextPoint.y, -1.0f));
        }
    }

    private void RemoveThrowArk()
    { _LineRenderer.positionCount = 0; }
}
using UnityEngine;

public class AbilityThrowNew : MonoBehaviour
{
    public HookResponderReference _HookResponder;
    public Vector3ComplexReference _Aim;

    public Transform _PickUpAttachTransform;

    public FloatReference _ThrowHeight;
    public IntReference _ThrowSegmentAmount;
    public FloatReference _ThrowRange;

    public AnimationCurve _DistanceHeightCurve;
    public AnimationCurve _DistanceTimeCurve;

    public GameEvent _OnPickUp;
    public GameEvent _OnThrow;

    public bool IsAiming()
    { return (!_Aim.Value.Direction.Equals(Vector3.zero)); }

    public void PickUp()
    {
        _OnPickUp.Raise();
        _HookResponder.Value._Rigidbody.isKinematic = true;
        _HookResponder.Value.AttachToTransform(_PickUpAttachTransform);
    }

    public void Throw()
    {
        if (!IsAiming()) return;

        _OnThrow.Raise();

        float flyTime = _DistanceTimeCurve.Evaluate(_Aim.Value.Magnitude / _ThrowRange.Value);
        _HookResponder.Value.StartThrow(flyTime / _ThrowSegmentAmount.Value, CalculateThrowArkPath());
        _HookResponder.Value = null;
    }

    public ThrowArkData CalculateThrowArkData()
    {
        float throwHeight = _DistanceHeightCurve.Evaluate(_Aim.Value.Magnitude / _ThrowRange.Value);

        Vector3 displacement = _Aim.Value.EndPoint - _HookResponder.Value.transform.position;
        float time = Mathf.Sqrt(-2 * throwHeight / Physics.gravity.y) + Mathf.Sqrt(2 * (displacement.y - throwHeight) / Physics.gravity.y);
        Vector3 velocity = new Vector3(displacement.x / time, Mathf.Sqrt(-2 * Physics.gravity.y * throwHeight), displacement.z / time);
        return new ThrowArkData(velocity * -Mathf.Sign(Physics.gravity.y), time);
    }

    public struct ThrowArkData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public ThrowArkData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

    public Vector3[] CalculateThrowArkPath()
    {
        Vector3[] points = new Vector3[_ThrowSegmentAmount.Value + 1];
        ThrowArkData launchData = CalculateThrowArkData();

        for (int i = 0; i < points.Length; i++)
        {
            float simulationTime = i / (float)_ThrowSegmentAmount.Value * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * Physics.gravity.y * simulationTime * simulationTime / 2f;
            points[i] = _HookResponder.Value.transform.position + displacement;
        }
        return points;
    }
}
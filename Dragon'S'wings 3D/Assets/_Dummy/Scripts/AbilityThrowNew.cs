using UnityEngine;

public class AbilityThrowNew : MonoBehaviour
{
    public HookResponderReference _HookResponder;
    public Vector3ComplexReference _Aim;

    public Transform _PickUpAttachTransform;

    public FloatReference _ThrowHeight;
    public IntReference _ThrowSegmentAmount;
    public FloatReference _ThrowRange;

    private Vector3[] _CurrentThrowPath;

    public AnimationCurve _DistanceHeightCurve;
    public AnimationCurve _DistanceTimeCurve;

    public GameEvent _OnPickUp;
    public GameEvent _OnThrow;

    public bool IsAiming()
    { return (!_Aim.Value.Direction.Equals(Vector2.zero)); }

    public void PickUp()
    {
        _OnPickUp.Raise();
        _HookResponder.Value._Rigidbody.isKinematic = true;
        _HookResponder.Value.AttachToObject(_PickUpAttachTransform);
    }

    public void Throw()
    {
        if (!IsAiming()) return;

        _OnThrow.Raise();
        _CurrentThrowPath = CalculateThrowArkPath();
        _HookResponder.Value._Rigidbody.useGravity = false;
        _HookResponder.Value._Rigidbody.isKinematic = false;
        _HookResponder.Value._PushBox.gameObject.SetActive(true);
        _HookResponder.Value.DetachFromObject();
        // _HookResponder.Value.GetComponentInParent<Rigidbody>().velocity = CalculateThrowArkData().initialVelocity;
        System.Collections.IEnumerator currentThrowRoutine = ThrowRoutine();
        StartCoroutine(currentThrowRoutine);
    }

    private System.Collections.IEnumerator ThrowRoutine()
    {
        float flyTime = _DistanceTimeCurve.Evaluate(_Aim.Value.Magnitude / _ThrowRange.Value);
        Debug.Log("Fly Time:" + flyTime);
        float timePerSegment = flyTime / _ThrowSegmentAmount.Value;
        Debug.Log("Time Per Segment: " + timePerSegment);
        for (int i = 1; i < _CurrentThrowPath.Length; i++)
        {
            // Debug.Log("Difference to should-be-position: " + (_CurrentThrowPath[i - 1] - _HookResponder.Value.transform.position));
            //    Vector3 directionVector = _CurrentThrowPath[i] - _HookResponder.Value.transform.position;
            //    Vector3 velocityVector = directionVector / timePerSegment;
            //    _HookResponder.Value._Rigidbody.velocity = velocityVector;
            //    Debug.Log(velocityVector);

            _HookResponder.Value._Rigidbody.MovePosition(_CurrentThrowPath[i]);
            yield return new WaitForSeconds(timePerSegment);
        }
        _HookResponder.Value._Rigidbody.useGravity = true;
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
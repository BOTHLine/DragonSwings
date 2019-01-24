using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AbilityThrowNew : MonoBehaviour
{
    public HookResponderReference _HookResponder;
    public Vector3ComplexReference _Aim;

    public FloatReference _ThrowHeight;
    public FloatReference _ThrowGravity;

    public bool debugPath;

    public bool launch = false;
    public bool reset = false;

    private LineRenderer _LineRenderer;
    public IntReference _ThrowSegmentAmount;
    public FloatReference _ThrowArkThicknessStart;
    public FloatReference _ThrowArkThicknessEnd;


    private void Awake()
    {
        _LineRenderer = GetComponent<LineRenderer>();
        _LineRenderer.startWidth = _ThrowArkThicknessStart.Value;
        _LineRenderer.endWidth = _ThrowArkThicknessEnd.Value;
    }

    void Start()
    {

    }

    void Update()
    {
        if (reset)
        {
            resetBox();
            reset = false;
        }


        if (launch)
        {
            RemovePath();
            debugPath = false;
            Launch();
            launch = false;
        }

        if (debugPath)
        {
            DrawPath();
        }
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * _ThrowGravity.Value;
        _HookResponder.Value.GetComponentInParent<Rigidbody>().velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {
        /*
        float displacementY = _Aim.Value.EndPoint.y - _HookResponder.Value.transform.position.y;
        Vector3 displacementXZ = new Vector3(_Aim.Value.EndPoint.x - _HookResponder.Value.transform.position.x, 0, _Aim.Value.EndPoint.z - _HookResponder.Value.transform.position.z);
        float time = Mathf.Sqrt(-2 * _ThrowHeight.Value / _ThrowGravity.Value) + Mathf.Sqrt(2 * (displacementY - _ThrowHeight.Value) / _ThrowGravity.Value);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _ThrowGravity.Value * _ThrowHeight.Value);
        Vector3 velocityXZ = displacementXZ / time;
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(_ThrowGravity.Value), time);
        */
        Vector3 displacement = _Aim.Value.EndPoint - _HookResponder.Value.transform.position;
        Debug.Log("Displacement: " + displacement.y);
        float time = Mathf.Sqrt(-2 * _ThrowHeight.Value / _ThrowGravity.Value) + Mathf.Sqrt(2 * (displacement.y - _ThrowHeight.Value) / _ThrowGravity.Value);
        Vector3 velocity = new Vector3(displacement.x / time, Mathf.Sqrt(-2 * _ThrowGravity.Value * _ThrowHeight.Value), displacement.z / time);
        return new LaunchData(velocity * -Mathf.Sign(_ThrowGravity.Value), time);
    }

    void DrawPath()
    {

        _LineRenderer.positionCount = _ThrowSegmentAmount.Value + 1;
        LaunchData launchData = CalculateLaunchData();
        //Vector3 previousDrawPoint = boxRigidbody.position;

        for (int i = 0; i < _LineRenderer.positionCount; i++)
        {
            float simulationTime = i / (float)_ThrowSegmentAmount.Value * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * _ThrowGravity.Value * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = _HookResponder.Value.transform.position + displacement;
            //   Debug.Log(displacement);
            _LineRenderer.SetPosition(i, drawPoint);
            //Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            //previousDrawPoint = drawPoint;
        }

        Vector3[] positions = new Vector3[_LineRenderer.positionCount];
        _LineRenderer.GetPositions(positions);

        foreach (Vector3 position in positions)
        {
            //   Debug.Log(position);
        }
    }

    void RemovePath()
    {
        _LineRenderer.positionCount = 0;
    }


    void resetBox()
    {
        debugPath = true;
        _HookResponder.Value.transform.parent.position = transform.position;
        _HookResponder.Value.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        _HookResponder.Value.GetComponentInParent<Rigidbody>().angularVelocity = Vector3.zero;
    }


    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
}
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ThrowingLauncher : MonoBehaviour
{
    public GameObject box;
    private Rigidbody boxRigidbody;
    public GameObject target;
    private Transform targetTransform;

    public float h;
    public float gravity;

    public bool debugPath;

    public bool launch = false;
    public bool reset = false;

    private LineRenderer _LineRenderer;
    public int _ThrowSegmentAmount;
    public float _ThrowArkThicknessStart;
    public float _ThrowArkThicknessEnd;

    private bool hasBox = true;

    private void Awake()
    {
        _LineRenderer = GetComponent<LineRenderer>();
        _LineRenderer.startWidth = _ThrowArkThicknessStart;
        _LineRenderer.endWidth = _ThrowArkThicknessEnd;

        boxRigidbody = box.GetComponent<Rigidbody>();
        targetTransform = target.transform;

        boxRigidbody.useGravity = false;

        reset = true;
    }

    private void Update()
    {
        if (reset)
        {
            resetBox();
            reset = false;
            hasBox = true;
        }

        if (hasBox)
        { box.transform.position = transform.position; }

        if (launch)
        {
            RemovePath();
            debugPath = false;
            Launch();
            launch = false;
            hasBox = false;
        }

        if (debugPath)
        { DrawPath(); }
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        boxRigidbody.useGravity = true;
        boxRigidbody.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = targetTransform.position.y - boxRigidbody.position.y;
        Vector3 displacementXZ = new Vector3(targetTransform.position.x - boxRigidbody.position.x, 0, targetTransform.position.z - boxRigidbody.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        _LineRenderer.positionCount = _ThrowSegmentAmount + 1;
        LaunchData launchData = CalculateLaunchData();
        //Vector3 previousDrawPoint = boxRigidbody.position;

        int resolution = 30;
        for (int i = 0; i < _LineRenderer.positionCount; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = boxRigidbody.position + displacement;
            _LineRenderer.SetPosition(i, drawPoint);
            //Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            //previousDrawPoint = drawPoint;
        }
    }

    void RemovePath()
    { _LineRenderer.positionCount = 0; }

    void resetBox()
    {
        debugPath = true;
        box.transform.position = transform.position;
        boxRigidbody.velocity = Vector3.zero;
        boxRigidbody.angularVelocity = Vector3.zero;
        boxRigidbody.useGravity = false;
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

    public void fire()
    { launch = true; }

    public void resetThrow()
    { reset = true; }
}
using System.Collections;
using UnityEngine;

public class ShortStopHook : MonoBehaviour
{
    public GameObject target;
    private Transform targetTransform;

    public float maxRange;
    public float slerpTime;

    public float rayCastThickness;

    private bool hasFired = false;
    private Vector3 lastPlayerPosition;

    public float flySpeed;
    private Vector3 hookPosition;

    public CharacterWalk myWalk;

    private LineRenderer _LineRenderer;
    public float _ThrowArkThicknessStart;
    public float _ThrowArkThicknessEnd;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = target.transform;

        _LineRenderer = GetComponent<LineRenderer>();
        _LineRenderer.startWidth = _ThrowArkThicknessStart;
        _LineRenderer.endWidth = _ThrowArkThicknessEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFired)
        {
            float RStickHorizontal = Input.GetAxis("Horizontal");
            float RStickVertical = -(Input.GetAxis("Vertical"));

            Vector3 playerMoved = lastPlayerPosition - transform.position;
            float currentAimDistance = ((new Vector2(RStickHorizontal, RStickVertical)).magnitude) * maxRange;

            Vector3 finalPosition = (transform.position + (new Vector3(RStickHorizontal, 0f, RStickVertical)).normalized * currentAimDistance);

            //spielerbewegung hart reinrechnen!
            target.transform.position -= playerMoved;

            //slerp hinterher
            target.transform.position = Vector3.Slerp(target.transform.position, finalPosition, slerpTime * Time.deltaTime);

            if ((target.transform.position - transform.position).magnitude > maxRange)
            {
                target.transform.position = (transform.position + (new Vector3(RStickHorizontal, 0f, RStickVertical)).normalized * maxRange);
            }

            Vector3 direction = target.transform.position - transform.position;
            float range = direction.magnitude;
            RaycastHit raycastHit;
            Physics.SphereCast(transform.position, rayCastThickness, direction, out raycastHit, range - rayCastThickness, 13);

            if (raycastHit.collider != null && raycastHit.transform.tag == "Wall")
            {
                Debug.Log("Wandtreffer");
                target.transform.position = raycastHit.point;
                hookPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            }

            else
            {
                Vector3 rayCastProbePosition = target.transform.position + Vector3.up;

                Physics.Raycast(rayCastProbePosition, Vector3.down, out raycastHit, 4f, 13);

                if (raycastHit.collider != null && raycastHit.transform.tag == "Pit")
                { hookPosition = Vector3.zero; }
                else if (raycastHit.collider != null && raycastHit.transform.tag == "Boden")
                { hookPosition = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z); }
                else
                { hookPosition = Vector3.zero; }
            }
        }
        lastPlayerPosition = transform.position;


        if (Input.GetAxis("Fire1") != 0 && !hasFired && !hookPosition.Equals(Vector3.zero))
        {
            hasFired = true;
            StartCoroutine(flyToTarget());
            target.transform.Find("Sphere").gameObject.GetComponent<Renderer>().enabled = false;
            target.transform.Find("Sphere (1)").gameObject.GetComponent<Renderer>().enabled = false;
        }

        _LineRenderer.positionCount = 0;
        _LineRenderer.positionCount = 2;
        _LineRenderer.SetPosition(0, transform.position);
        _LineRenderer.SetPosition(1, target.transform.position);
    }

    IEnumerator flyToTarget()
    {
        myWalk.walkingIsAllowed = false;
        bool notThere = true;
        while (notThere)
        {
            transform.position = Vector3.MoveTowards(transform.position, hookPosition, flySpeed * Time.deltaTime);

            if ((transform.position - hookPosition).magnitude < 0.001f) notThere = false;

            yield return new WaitForFixedUpdate();
        }

        hasFired = false;
        target.transform.Find("Sphere").gameObject.GetComponent<Renderer>().enabled = true;
        target.transform.Find("Sphere (1)").gameObject.GetComponent<Renderer>().enabled = true;
        myWalk.walkingIsAllowed = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMyBox : MonoBehaviour, Aimable
{
    public float height;
    public float flyTime;
    public float holdingOffset;

    public GameObject shadow;

    public GameObject myOldParent;
    private GameObject myNewParent;

    private Vector3 startPoint;
    private Vector3 targetPosition;
    private Vector2 startPosi;
    private Vector2 targetPosi;

    private Vector3 shadowOriginalScale;
    private Vector3 currentScale;

    public bool throwNow = false;
    public bool flying = false;
    private float flyCounter = 0f;

    private bool isMovingToPlayer = false;
    private bool isMovingAwayFromPlayer = false;



    private LineRenderer playerLine;
    private GameObject lineHolder;

    public FloatReference _Damage;

    private void Awake()
    {
        myOldParent = transform.parent.gameObject;
    }

    // Use this for initialization
    void Start()
    {
        shadowOriginalScale = shadow.transform.lossyScale;

        lineHolder = new GameObject("lineHolder");
        lineHolder.transform.parent = this.gameObject.transform;

        playerLine = lineHolder.AddComponent<LineRenderer>();
        playerLine.startWidth = 0.03f;
        playerLine.endWidth = 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        if (throwNow && !flying)
        {
            flyCounter = 0f;

            //targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition) + new Vector3 (0,0,9); //Vector plus um z = -10 der cam auszugleichen
            startPoint = this.transform.position;

            targetPosi = new Vector2(targetPosition.x, targetPosition.y);
            startPosi = new Vector2(startPoint.x, startPoint.y);

            flying = true;
            throwNow = false;
        }

        if (flying)
        {
            flyTheBox();
            scaleTheShadow();
        }
    }

    public void movingToPlayer(Vector3 throwingHitPosition, GameObject futurParent)
    {
        isMovingToPlayer = true;
        myNewParent = futurParent;

        if (!throwNow && !flying)
        {

            throwingHitPosition = new Vector3(throwingHitPosition.x, throwingHitPosition.y + holdingOffset, 0);

            targetPosition = throwingHitPosition;
            throwNow = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void throwingAwayFromPlayer(Vector3 throwingHitPosition)
    {
        isMovingAwayFromPlayer = true;

        gameObject.transform.parent = myOldParent.transform;
        shadow.gameObject.transform.parent = myOldParent.transform;

        if (!throwNow && !flying)
        {
            throwingHitPosition = new Vector3(throwingHitPosition.x, throwingHitPosition.y, 0);

            targetPosition = throwingHitPosition;
            throwNow = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void flyTheBox()
    {
        Vector2 current = SampleParabola(startPosi, targetPosi, height, flyCounter / flyTime);
        this.transform.position = new Vector3(current.x, current.y, -0);

        flyCounter++;

        shadow.transform.position = Vector3.Lerp(startPoint, isMovingToPlayer ? new Vector3(targetPosition.x, targetPosition.y - holdingOffset, 0) : targetPosition, flyCounter / flyTime);
        //shadow.transform.localScale *= ;
        if ((transform.position - targetPosition).sqrMagnitude < 0.0001)
        {
            if (isMovingToPlayer)
            {
                gameObject.transform.parent = myNewParent.transform;
                shadow.gameObject.transform.parent = myNewParent.transform;

                gameObject.transform.position = new Vector3(myNewParent.transform.position.x, myNewParent.transform.position.y + holdingOffset, 0);
                shadow.gameObject.transform.position = new Vector3(myNewParent.transform.position.x, myNewParent.transform.position.y, 0);
                //shadow.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

                isMovingToPlayer = false;
                gameObject.layer = (int)Layer.PlayerProjectile;
            }

            if (isMovingAwayFromPlayer)
            {
                gameObject.transform.position = targetPosi;
                shadow.gameObject.transform.position = targetPosi;

                isMovingAwayFromPlayer = false;
                gameObject.layer = (int)Layer.Object;
            }
            flying = false;
            DealDamage();
        }
    }

    public void scaleTheShadow()
    {
        float fracJourney = flyCounter / flyTime;

        if (fracJourney <= 0.5)
        {
            shadow.transform.localScale = shadowOriginalScale - shadowOriginalScale * (fracJourney);
            currentScale = transform.localScale;
        }
        else
        { shadow.transform.localScale = shadowOriginalScale * (fracJourney); }

        if (!isMovingToPlayer)
        { shadow.transform.localScale = shadowOriginalScale * 0.7f; }
    }
    public Vector2 SampleParabola(Vector2 start, Vector2 end, float height, float t)
    {
        float parabolicT = t * 2 - 1;
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector2 travelDirection = end - start;
            Vector2 result = start + t * travelDirection;
            result.y += (-parabolicT * parabolicT + 1) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector2 travelDirection = end - start;
            Vector2 levelDirecteion = end - new Vector2(start.x, end.y);
            Vector2 up = new Vector2(0.0f, 1.0f);
            //if (end.y > start.y) up = -up;
            Vector2 result = start + t * travelDirection;
            result += ((-parabolicT * parabolicT + 1) * height) * up;
            return result;
        }
    }

    public void drawArk(Vector3 startPoint, Vector3 endPoint)
    {
        int arkSegmentsCount = 30;

        playerLine.positionCount = arkSegmentsCount + 1;


        float steps = ((endPoint - startPoint).magnitude) / arkSegmentsCount;

        for (int i = 0; i <= arkSegmentsCount; i++)
        {


            Vector3 nextPoint = SampleParabola(startPoint, endPoint, height, i / (float)arkSegmentsCount);

            playerLine.SetPosition(i, new Vector3(nextPoint.x, nextPoint.y, -1));






        }


    }

    public void destroyAllLines()
    {
        playerLine.positionCount = 0;
    }

    private void DealDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f, LayerList.PlayerProjectile.LayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            HurtBox hurtBox = colliders[i].GetComponent<HurtBox>();
            Debug.Log(hurtBox);
            if (hurtBox != null)
            {
                hurtBox.Hurt(_Damage);
            }
        }
    }
}
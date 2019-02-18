using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingTheWayToGo : MonoBehaviour
{
    public GameObject hiddenTarget;
    public GameObject target;

    private Vector3 lastPlayerPosition;

    public float maxRange;
    public float slerpTime;
    public float angularSlerpTime;

    public bool ignoreEnemies;

    public LineRenderer myLine;
    public float lineThickness;

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPosition = transform.position;


        myLine.startWidth = lineThickness;
        myLine.endWidth = lineThickness;
        myLine.SetVertexCount(2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerMoved = lastPlayerPosition - transform.position;
        hiddenTarget.transform.position -= playerMoved;
        myLine.SetPosition(0, transform.position);

        Vector3 currentAim = hiddenTarget.transform.position - transform.position;
        Vector3 newAim = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, -Input.GetAxisRaw("Vertical"));

        Vector3 currentAimDirection = currentAim.normalized;
        Vector3 newAimDirection = newAim.normalized;

        float currentAimLength = currentAim.magnitude;
        float newAimLength = Mathf.Clamp01(newAim.magnitude) * maxRange;
        //Debug.Log("Current Aim Length: " + currentAimLength);
        //Debug.Log("New Aim Length: " + newAimLength);

        float calculatedAimLength = Mathf.Lerp(currentAimLength, newAimLength, slerpTime * Time.deltaTime);
        //Debug.Log("Calculated Aim Length: " + calculatedAimLength);
        Vector3 calculatedAimDirection = Vector3.Slerp(currentAimDirection, newAimDirection, angularSlerpTime * Time.deltaTime);
        Vector3 calculatedAim = new Vector3(calculatedAimDirection.x, 0f, calculatedAimDirection.z).normalized * calculatedAimLength;

        hiddenTarget.transform.position = transform.position + calculatedAim;

        float currentMaxRange = 0;

        LayerMask myMask = 13;

        RaycastHit raycastHitWalls;
        Physics.SphereCast(transform.position, 0.3f, calculatedAimDirection, out raycastHitWalls, calculatedAimLength - 0.3f, myMask);
        //Physics.Raycast(transform.position, calculatedAimDirection, out raycastHitWalls, (transform.position - hiddenTarget.transform.position).magnitude, myMask);


        if (raycastHitWalls.collider != null && (raycastHitWalls.transform.tag == "Wall" || (!ignoreEnemies && raycastHitWalls.transform.tag == "Enemy") || raycastHitWalls.transform.tag == "Box"))
        {
            currentMaxRange = (raycastHitWalls.point - transform.position).magnitude;
            target.transform.position = transform.position + (hiddenTarget.transform.position - transform.position).normalized * currentMaxRange;
        }



        else
        {
            target.transform.position = hiddenTarget.transform.position;
        }


        //----------
        myLine.SetPosition(1, target.transform.position);
        lastPlayerPosition = transform.position;
    }
}
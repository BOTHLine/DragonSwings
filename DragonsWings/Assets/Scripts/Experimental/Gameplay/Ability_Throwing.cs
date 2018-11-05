using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Throwing : MonoBehaviour
{
    public GameObject aim;
    public FloatReference range;


    public GameObject currentObject;


    private bool gotSomething = false;
    private int layerMask = 1 << 13;
    // Use this for initialization
    void Start()
    {

    }
    
    public void HandleThrow()
    {
        if (!gotSomething)
        {
            currentObject = getTarget();

            if (currentObject != null) gotSomething = true;

        }
        else
        {
            if (currentObject != null && currentObject.GetComponent<ThrowMyBox>() != null && !currentObject.GetComponent<ThrowMyBox>().flying)
            {

                RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, aim.transform.position - transform.parent.position, range, layerMask);
                if (raycasthit.collider)
                {

                    currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(raycasthit.point);

                }

                else

                {
                    currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(transform.position + (aim.transform.position - transform.position).normalized * range);
                }

                currentObject = null;
                gotSomething = false;
            }
        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if (!gotSomething)
        {
            if (Input.GetAxis("Axis10") > 0)
            {
                currentObject = getTarget();

                if (currentObject != null) gotSomething = true;



            }
        }


        else
        {
            if (currentObject != null && currentObject.GetComponent<ThrowMyBox>() != null && !currentObject.GetComponent<ThrowMyBox>().flying && Input.GetAxis("Axis10") > 0)
            {

                RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, aim.transform.position - transform.parent.position, range, layerMask);
                if (raycasthit.collider)
                {

                    currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(raycasthit.point);

                }

                else

                {
                    currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(transform.position + (aim.transform.position - transform.position).normalized * range);
                }

                currentObject = null;
                gotSomething = false;
            }
        }
    }
    */

    public GameObject getTarget()
    {
        bool hitSomething = false;
        GameObject currentTarget = null;

        RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, aim.transform.position - transform.parent.position, range, layerMask);
        if (raycasthit.collider)
        {

            //result = (raycasthit.transform.position - transform.parent.position).magnitude;

            if (raycasthit.transform.tag == "Box" && raycasthit.collider.gameObject.GetComponent<ThrowMyBox>())
            {
                Debug.Log("hit");
                currentTarget = raycasthit.transform.gameObject;

                if (currentTarget.GetComponent<ThrowMyBox>() && !currentTarget.GetComponent<ThrowMyBox>().flying)
                {
                    hitSomething = true;

                    currentTarget.GetComponent<ThrowMyBox>().movingToPlayer(transform.position, gameObject);

                }
            }

        }

        if (!hitSomething) return null;
        else return currentTarget;
    }





}

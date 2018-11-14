using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Throwing : MonoBehaviour
{
    public GameObject aim;
    public FloatReference range;

    public GameObject currentObject;

<<<<<<< HEAD
=======
    
>>>>>>> master
    private bool gotSomething = false;
    private int layerMask = 1 << 13;

<<<<<<< HEAD
    public void HandleThrow()
    {
=======

     


    }
    
    public void HandleThrow()
    {
        
>>>>>>> master
        if (!gotSomething)
        {
            currentObject = getTarget();

            if (currentObject != null)
            { gotSomething = true; }
        }
        else
        {
            if (currentObject != null && currentObject.GetComponent<ThrowMyBox>() != null && !currentObject.GetComponent<ThrowMyBox>().flying)
            {
                RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, aim.transform.position - transform.parent.position, range, layerMask);
<<<<<<< HEAD

                if (raycasthit.collider)
                { currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(raycasthit.point); }
                else { currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(transform.position + (aim.transform.position - transform.position).normalized * range); }
=======
                if (raycasthit.collider)
                {

                    currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(raycasthit.point);

                    currentObject.GetComponent<ThrowMyBox>().destroyAllLines();
                }

                else
                {
                    currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(transform.position + (aim.transform.position - transform.position).normalized * range);

                    currentObject.GetComponent<ThrowMyBox>().destroyAllLines();
                }
>>>>>>> master

                currentObject = null;
                gotSomething = false;


                //Linie Malen




            }
        }
<<<<<<< HEAD
=======
        
>>>>>>> master
    }


    // Update is called once per frame
    void Update()
    {
        if (currentObject != null && currentObject.GetComponent<ThrowMyBox>() != null && !currentObject.GetComponent<ThrowMyBox>().flying)
        {
<<<<<<< HEAD
            if (Input.GetAxis("Axis10") > 0)
            {
                currentObject = getTarget();

                if (currentObject != null)
                { gotSomething = true; }
=======
            

            RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, aim.transform.position - transform.parent.position, range, layerMask);
            if (raycasthit.collider)
            {

                currentObject.GetComponent<ThrowMyBox>().drawArk(currentObject.transform.position, raycasthit.point);

>>>>>>> master
            }

<<<<<<< HEAD
        else
        {
            if (currentObject != null && currentObject.GetComponent<ThrowMyBox>() != null && !currentObject.GetComponent<ThrowMyBox>().flying && Input.GetAxis("Axis10") > 0)
            {
                RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, aim.transform.position - transform.parent.position, range, layerMask);

                if (raycasthit.collider)
                { currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(raycasthit.point); }
                else
                { currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(transform.position + (aim.transform.position - transform.position).normalized * range); }
=======
            else

            {
                currentObject.GetComponent<ThrowMyBox>().drawArk(currentObject.transform.position, transform.position + (aim.transform.position - transform.position).normalized * range);
            }


>>>>>>> master

        }
    }


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
                currentTarget = raycasthit.transform.gameObject;

                if (currentTarget.GetComponent<ThrowMyBox>() && !currentTarget.GetComponent<ThrowMyBox>().flying)
                {
                    hitSomething = true;
                    currentTarget.GetComponent<ThrowMyBox>().movingToPlayer(transform.position, gameObject);
                }
            }
        }

        if (!hitSomething)
        { return null; }
        else { return currentTarget; }
    }
<<<<<<< HEAD
}
=======


   


}
>>>>>>> master

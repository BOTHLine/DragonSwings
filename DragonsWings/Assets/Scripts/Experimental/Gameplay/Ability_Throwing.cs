using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Throwing : MonoBehaviour
{
    public Vector2Reference _AimPosition;
    public FloatReference range;


    public GameObject currentObject;

    public BoolReference PlayerHasSomethingInHand;
    //  private int layerMask = 1 << 13;

    public void HandleThrow()
    {

        if (!PlayerHasSomethingInHand.Value)
        {
            currentObject = getTarget();

            if (currentObject != null) PlayerHasSomethingInHand.Value = true;
        }
        else
        {
            if (currentObject != null && currentObject.GetComponent<ThrowMyBox>() != null && !currentObject.GetComponent<ThrowMyBox>().flying)
            {
                RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, _AimPosition - (Vector2)transform.parent.position, range, LayerList.PlayerProjectile.LayerMask);
                if (raycasthit.collider)
                {

                    currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(raycasthit.point);
                    currentObject.GetComponent<ThrowMyBox>().destroyAllLines();
                }

                else
                {
                    currentObject.GetComponent<ThrowMyBox>().throwingAwayFromPlayer(transform.position + ((Vector3)_AimPosition.Value - transform.position).normalized * range);
                    currentObject.GetComponent<ThrowMyBox>().destroyAllLines();
                }

                currentObject = null;
                PlayerHasSomethingInHand.Value = false;

                //Linie Malen
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (currentObject != null && currentObject.GetComponent<ThrowMyBox>() != null && !currentObject.GetComponent<ThrowMyBox>().flying)
        {
            RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, _AimPosition - (Vector2)transform.parent.position, range, LayerList.PlayerProjectile.LayerMask);
            if (raycasthit.collider)
            {
                currentObject.GetComponent<ThrowMyBox>().drawArk(currentObject.transform.position, raycasthit.point);
            }
            else
            {
                currentObject.GetComponent<ThrowMyBox>().drawArk(currentObject.transform.position, transform.position + ((Vector3)_AimPosition.Value - transform.position).normalized * range);
            }
        }
    }


    public GameObject getTarget()
    {
        bool hitSomething = false;
        GameObject currentTarget = null;

        RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, _AimPosition - (Vector2)transform.parent.position, range, LayerList.PlayerProjectile.LayerMask);
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
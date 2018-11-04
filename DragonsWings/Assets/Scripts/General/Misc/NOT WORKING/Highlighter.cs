using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public GameObject aim;

    public FloatReference range;

    public int toLayerMask = 8;

    private Outliner currentHighlighted;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        isSomeThingHighlightableThenDoSo();

    }



    private void isSomeThingHighlightableThenDoSo()
    {

        int layerMask = 8;

        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.parent.position, aim.transform.position - transform.parent.position, range, toLayerMask);
        if (raycastHit2D.collider)
        {
            if (raycastHit2D.transform.tag == "Vase" || raycastHit2D.transform.tag == "Box")
            {
                if (raycastHit2D.collider)
                {
                   
         
                    Outliner test = raycastHit2D.collider.GetComponent<Outliner>();
                    if (test)
                    {
                        if (currentHighlighted)
                        {
                            if (test != currentHighlighted)
                            {
                                currentHighlighted.SetHighlight(false);
                                test.SetHighlight(true);
                                currentHighlighted = test;
                                Debug.Log("wurde gehighlightet");
                            }
                        }
                        else
                        {
                            test.SetHighlight(true);
                            currentHighlighted = test;
                        }
                    }
                    else if (currentHighlighted)
                    {
                        currentHighlighted.SetHighlight(false);
                        currentHighlighted = null;
                    }
                }
                else if (currentHighlighted)
                {
                    currentHighlighted.SetHighlight(false);
                    currentHighlighted = null;
                }
            }

            else
            {
                if (currentHighlighted != null)
                {
                    currentHighlighted.SetHighlight(false);
                    currentHighlighted = null;
                }
            }



        }     
   
    }





}

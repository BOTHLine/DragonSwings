using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBox : MonoBehaviour {


    public int hp = 6;

    public Sprite damaged01;
    public Sprite damaged02;
    public Sprite destroyed;

    public GameObject visualShadowStandingAround;
    public GameObject shadow;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Vase")|| other.CompareTag("Box"))
        {
            takeDmg();
        }

    }


    public void takeDmg()
    {
        hp--;

        if (hp == 4)
        {
            gameObject.transform.GetComponent<SpriteRenderer>().sprite = damaged01;
        }

        if (hp == 2)
        {
            gameObject.transform.GetComponent<SpriteRenderer>().sprite = damaged02;
        }



        if (hp <= 0)
        {

            shadow.transform.GetComponent<Renderer>().enabled = false;
            visualShadowStandingAround.transform.GetComponent<Renderer>().enabled = false;

            Destroy(gameObject.transform.GetComponent<BoxCollider2D>());
            Destroy(gameObject.transform.GetComponent<PolygonCollider2D>());


            gameObject.transform.GetComponent<SpriteRenderer>().sprite = destroyed;
            gameObject.transform.GetComponent<SpriteRenderer>().sortingOrder = 23;

            //Destroy(this.gameObject);
        }


    }

}

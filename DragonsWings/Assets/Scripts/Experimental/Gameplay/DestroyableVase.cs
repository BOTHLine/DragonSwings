using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableVase : MonoBehaviour
{


    public int hp;

    public Sprite destroyed01;
    public Sprite destroyed02;
    public GameObject myShadow;


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

        if (other.CompareTag("Box"))
        {
            takeDmg();
        }
    }


    public void takeDmg()
    {
        hp--;

        if (hp <= 0)
        {

            Destroy(gameObject.transform.GetComponent<CircleCollider2D>());
            Destroy(gameObject.transform.GetComponent<PolygonCollider2D>());

            myShadow.transform.GetComponent<Renderer>().enabled = false;

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Random.Range(0, 2) == 1 ? destroyed01 : destroyed02;
            spriteRenderer.sortingLayerName = "On Ground";
            // TODO Hard coded LayerName
            
            //Destroy(this.gameObject);
        }


    }


}

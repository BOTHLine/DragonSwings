using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    public List<GameObject> GateList = new List<GameObject>();

    public Sprite openPic;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool emptycheck = false;

        if (EnemyList.Count != 0)
        {
            emptycheck = true;
            foreach (GameObject current in EnemyList)
            {
                if (current.activeInHierarchy) emptycheck = false;
            }

        }

        if (emptycheck)
        {
            foreach (GameObject current in GateList)
            {
                current.transform.GetComponent<BoxCollider2D>().enabled = false;
                current.transform.Find("SpriteRenderer").GetComponent<SpriteRenderer>().sprite = openPic;
                current.transform.Find("SpriteRenderer").GetComponent<SpriteRenderer>().sortingOrder = 10;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTheFun : MonoBehaviour
{

    private GameObject player;


    public List<GameObject> PlayerPositionList = new List<GameObject>();

    private List<Vector3> positionsList = new List<Vector3>();


    public void Start()
    {
       player = GameObject.Find("Player");

       positionsList.Add(player.transform.position);
       positionsList.Add(player.transform.position);

        fillTheList();

    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetSceneKeyPoint(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSceneKeyPoint(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSceneKeyPoint(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSceneKeyPoint(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetSceneKeyPoint(4);
        }
    }

  

    public void SetSceneKeyPoint(int position)
    {
        if (position > -1 && position < positionsList.Count)
        {
            player.transform.position = positionsList[position];           
        }

    }


    private void fillTheList()
    {
        foreach (GameObject current in PlayerPositionList)
        {
            positionsList.Add(current.transform.position);
        }
    }

}

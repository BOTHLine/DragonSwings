﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    /*
    public GameObject playerGameObject;
    private Transform target;
    private Rigidbody2D playerRigidBody;

    private Camera myCam;
    private Vector2 screenSize;
    private float camZCoord;

    public float smoothing;

    public Vector3 camOffset;
    public Vector3 upperLeftBoarder;
    public Vector3 lowerRightBoarder;

    private LineRenderer boarderLine;

    public bool forshadowCam = false;
    public float forshadowRate = 0.5f;

    public float maxCamToPlayerDistance;
    public float yAxisCanForeshadowFactor;



    private float screenRatio;

    void Start()
    {
        camZCoord = transform.position.z;

        myCam = this.gameObject.transform.GetComponent<Camera>();

        //Achtung: Screen.width ist nur die halbe Kamerabildschirmbreite!
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenRatio = (float)Screen.height / (float)Screen.width;

        target = playerGameObject.transform;
        playerRigidBody = playerGameObject.GetComponent<Rigidbody2D>();

        boarderLine = this.gameObject.AddComponent<LineRenderer>();
        boarderLine.startWidth = 0.0f;
        boarderLine.endWidth = 0.0f;
        boarderLine.positionCount = 10;

        // camOffset = this.transform.position;

        initializeCam();

    }

    void FixedUpdate() //evtl doch lieber LateUpdate?
    {
        moveCam();
        drawMyBoarders();

    }





    public void moveCam()
    {
        Vector3 myPosi = this.transform.position;

        Vector3 desiredPosition = target.position + camOffset;


        if (forshadowCam)
        {
            desiredPosition = forshadow(desiredPosition);
        }

        desiredPosition = checkWorldBoarders(new Vector3(desiredPosition.x, desiredPosition.y, camZCoord));


        Vector3 smoothedPosition = Vector3.Lerp(transform.position, checkWorldBoarders(desiredPosition), smoothing * Time.deltaTime);

        transform.position = smoothedPosition;



    }

    /// <summary>
    /// Bewegt den Pointer, wo die kamera hinsliden möchte zurück in die berechneten Grenzen
    /// </summary>
    /// <param name="myPosi"></param>
    /// <returns></returns>
    public Vector3 checkWorldBoarders(Vector3 myPosi)
    {
        bool notAtCorners = true;
        //Grenze Oben links
        if (myPosi.x - screenSize.x < upperLeftBoarder.x)
        {
            myPosi.x = upperLeftBoarder.x + screenSize.x;
            notAtCorners = false;
        }

        if (myPosi.y + screenSize.y > upperLeftBoarder.y)
        {
            myPosi.y = upperLeftBoarder.y - screenSize.y;
            notAtCorners = false;
        }
        //Grenze unten rechts
        if (myPosi.x + screenSize.x > lowerRightBoarder.x)
        {
            myPosi.x = lowerRightBoarder.x - screenSize.x;
            notAtCorners = false;
        }
        if (myPosi.y - screenSize.y < lowerRightBoarder.y)
        {
            myPosi.y = lowerRightBoarder.y + screenSize.y;
            notAtCorners = false;
        }

        if (notAtCorners) myPosi = checkPlayerDistance(myPosi);

        myPosi = new Vector3(myPosi.x, myPosi.y, camZCoord);

        return myPosi;
    }


    public Vector3 checkPlayerDistance(Vector3 camPosi)
    {
        if ((camPosi - target.transform.position).magnitude > maxCamToPlayerDistance)
        {
            camPosi = target.transform.position + ((camPosi - target.transform.position).normalized * maxCamToPlayerDistance);
        }

        return camPosi;
    }


    /// <summary>
    /// Benutzt die Velocity des Players um mit der Kamera in die Bewegungsrichtung vorher zu schauen
    /// Dabei wird das Verhältnis des Bildschirmausschnittes berücksichtig
    /// maxCamToPlayerDistance sorgt dafür, dass der Spieler an den Rändern nicht aus dem Bild verschwindet, wenn er wieder zurück in das Szenen innere läuft
    /// </summary>
    /// <param name="actualPosition"></param>
    /// <returns></returns>
    public Vector3 forshadow(Vector3 actualPosition)
    {
        Vector3 result = actualPosition;

        Vector2 velocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.y);

        result = actualPosition + new Vector3(velocity.x, velocity.y * yAxisCanForeshadowFactor, 0) * forshadowRate;


        return result;
    }


    public void initializeCam()
    {
        Vector3 myPosi = this.transform.position;

        Vector3 desiredPosition = target.position + camOffset;
        desiredPosition = checkWorldBoarders(new Vector3(desiredPosition.x, desiredPosition.y, camZCoord));

        transform.position = desiredPosition;
    }



    private void drawMyBoarders()
    {
        boarderLine.SetPosition(0, upperLeftBoarder);
        boarderLine.SetPosition(1, new Vector2(lowerRightBoarder.x, upperLeftBoarder.y));
        boarderLine.SetPosition(2, lowerRightBoarder);
        boarderLine.SetPosition(3, new Vector2(upperLeftBoarder.x, lowerRightBoarder.y));
        boarderLine.SetPosition(4, upperLeftBoarder);

        boarderLine.SetPosition(5, new Vector2(upperLeftBoarder.x + screenSize.x, upperLeftBoarder.y - screenSize.y));
        boarderLine.SetPosition(6, new Vector2(lowerRightBoarder.x - screenSize.x, upperLeftBoarder.y - screenSize.y));
        boarderLine.SetPosition(7, new Vector2(lowerRightBoarder.x - screenSize.x, lowerRightBoarder.y + screenSize.y));
        boarderLine.SetPosition(8, new Vector2(upperLeftBoarder.x + screenSize.x, lowerRightBoarder.y + screenSize.y));
        boarderLine.SetPosition(9, new Vector2(upperLeftBoarder.x + screenSize.x, upperLeftBoarder.y - screenSize.y));
    }

    */
}

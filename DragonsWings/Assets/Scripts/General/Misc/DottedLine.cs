using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLine : MonoBehaviour
{
    public Vector2Reference _AimPosition;
    public GameObject aimingDot;

    public Vector3 startPoint;
    public Vector3 endPoint;
    public float spacing;
    //public float maxDistance;
    private List<GameObject> dots;
    private int lastCount = 0;
    public GameObject cursor;

    public bool shrinkDots = false;
    public float shrinkingRate;
    private Vector3 startScaleDots;

    public FloatReference range;


    void Start()
    {
        startPoint = gameObject.transform.parent.position;
        dots = new List<GameObject>();
        //cursor = aimingDot;
        startScaleDots = cursor.transform.localScale / 4;
        //cursor.transform.localScale = cursor.transform.localScale * 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Cam setzt die z Position auf -10 ... das fixe ich hiermit: ++ new Vector3 (0,0,10)
        //endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        endPoint = gameObject.transform.parent.position + (((Vector3)_AimPosition.Value - transform.parent.position).normalized * checkRange());

        cursor.transform.position = endPoint;

        if ((gameObject.transform.parent.position - endPoint).magnitude <= 0.001f) cursor.GetComponent<Renderer>().enabled = false;
        else cursor.GetComponent<Renderer>().enabled = true;

        drawLine();
    }

    public void drawLine()
    {
        startPoint = gameObject.transform.parent.position;

        //Richtungsvektor für die Linie
        Vector2 diff = endPoint - startPoint;
        float distancePoints = Vector3.Distance(endPoint, startPoint);

        //Längencap
        if (distancePoints > range) distancePoints = range;

        //Anzahl an Dots
        int count = (int)(distancePoints / spacing);
        //Verschiebung um Sprünge beim spawnen von neuen Punkten zu verhinden
        float offset = (distancePoints) % spacing;

        while (dots.Count < count)
        {
            GameObject newDot = (GameObject)Instantiate(aimingDot);
            newDot.transform.parent = this.transform;
            dots.Add(newDot);
        }

        // Um in der Nächsten Runde Punkte die zu weit draußen sind wieder auszusortieren
        if (lastCount > count)
        {
            for (int i = lastCount - 1; i > count - 1; i--)
            {

                dots[i].transform.position = new Vector3(10000, 10000, 0);

            }
        }

        Vector3 step = (diff.normalized * spacing);

        for (int i = 0; i < count; i++)
        {
            GameObject currentDot = dots[i];
            currentDot.transform.position = startPoint + new Vector3(diff.x, diff.y, 0).normalized * offset + (step * i);

            if (shrinkDots) currentDot.transform.localScale = startScaleDots - startScaleDots * (shrinkingRate * i);
        }

        // Um in der Nächsten Runde Punkte die zu weit draußen sind wieder auszusortieren
        lastCount = count;
    }

    //Hitdetection Coloring
    public void colorAllDots(Color newColor)
    {
        foreach (GameObject current in dots)
        {
            current.gameObject.GetComponent<SpriteRenderer>().color = newColor;
        }

        cursor.GetComponent<SpriteRenderer>().color = newColor;
    }


    public void resetColorOfDots()
    {
        colorAllDots(new Color(1, 1, 1, 0.8f));
    }


    public float checkRange()
    {
        float result = range;
        //    int layerMask = 1 << 13;

        //LayerList.Hook.LayerMask
        RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, (Vector3)_AimPosition.Value - transform.parent.position, range, LayerList.PlayerProjectile.LayerMask);
        if (raycasthit.collider)
        {
            //result = (raycasthit.transform.position - transform.parent.position).magnitude;
            if (raycasthit.transform.tag == "Vase") colorAllDots(new Color(1, 0, 0, 0.8f));
            else if (raycasthit.transform.tag == "Box" || raycasthit.transform.tag == "Wall") colorAllDots(new Color(0.043f, 0.4f, 0.137f, 0.8f));
            else if (raycasthit.transform.tag == "Enemy") colorAllDots(new Color(0.2f, 0.5f, 0.5f, 0.8f));
            else resetColorOfDots();

            result = raycasthit.distance;
        }

        else resetColorOfDots();

        return result;
    }
}
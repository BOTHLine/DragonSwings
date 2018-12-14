using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLine : MonoBehaviour
{
    public Vector2ComplexReference _Aim;
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

    public Color _NothingColor;
    public Color _EnemyColor;
    public Color _BoxColor;
    public Color _WallColor;
    public Color _VaseColor;

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

        endPoint = (Vector2)gameObject.transform.parent.position + _Aim.Value.Direction * checkRange();
        // Vector2Complex aim = _Aim;
        // endPoint = aim.EndPoint;

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
        if (distancePoints > range.Value) distancePoints = range.Value;

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
        colorAllDots(_NothingColor);
    }

    public float checkRange()
    {
        float result = range.Value;
        //    int layerMask = 1 << 13;

        //LayerList.Hook.LayerMask
        RaycastHit2D raycasthit = Physics2D.Raycast(transform.parent.position, _Aim.Value.Direction, range.Value, LayerList.PlayerProjectile.LayerMask);
        if (raycasthit.collider)
        {
            //result = (raycasthit.transform.position - transform.parent.position).magnitude;
            if (raycasthit.collider.tag == "Vase") colorAllDots(_VaseColor);
            else if (raycasthit.collider.tag == "Box") colorAllDots(_BoxColor);
            else if (raycasthit.collider.tag == "Wall") colorAllDots(_WallColor);
            else if (raycasthit.collider.tag == "Enemy") colorAllDots(_EnemyColor);
            else resetColorOfDots();

            result = raycasthit.distance;
        }

        else resetColorOfDots();

        return result;
    }
}
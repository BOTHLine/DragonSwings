using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{

    public float height;
    public float flySteps;
    private float currentFlyStep;

    public List<GameObject> lootTargets;

    private List<GameObject> loots;

    public GameObject lootExample;

    private bool lootIsFlying = false;
    private bool allThingsHasLanded = false;

    public bool spawnTheLootButton = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Das hie rmuss der Trigger aufrufen!
        if (spawnTheLootButton)
        {
            currentFlyStep = 0;
            spawnKonfetti();
            spawnTheLootButton = false;
            moveTargetsRandomAround();
        }


        if (lootIsFlying && !allThingsHasLanded)
        {
            currentFlyStep += flySteps;
            flyAllTheKonfetti(currentFlyStep);
        }


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spawnTheLootButton = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }


    public void spawnKonfetti()
    {
        loots = new List<GameObject>();

        foreach (GameObject current in lootTargets)
        {
            GameObject newLoot = Instantiate(lootExample, transform.position, Quaternion.identity);

            giveMeRandomColor(newLoot);

            loots.Add(newLoot);
        }

        lootIsFlying = true;

    }

    public void flyAllTheKonfetti(float flyStep)
    {
        bool nothingChanged = true;

        for (int i = 0; i < loots.Count; i++)
        {
            GameObject current = loots[i];


            Vector2 targetPosition = SampleParabola((Vector2)current.transform.position, (Vector2)lootTargets[i].transform.position, height, flyStep);

            current.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);

            changeMyRotation(current);


            if (((((Vector2)current.transform.position) - (Vector2)lootTargets[i].transform.position).magnitude) <= 0.1f) allThingsHasLanded = true;


            nothingChanged = false;
        }

        if (allThingsHasLanded)
        {
            lootIsFlying = false;
            allThingsHasLanded = false;

            // HIER ist die Animation beendet!!!!

        }
    }

    //Flugparabel für die schnipsel
    Vector2 SampleParabola(Vector2 start, Vector2 end, float height, float t)
    {
        float parabolicT = t * 2 - 1;
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector2 travelDirection = end - start;
            Vector2 result = start + t * travelDirection;
            result.y += (-parabolicT * parabolicT + 1) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector2 travelDirection = end - start;
            Vector2 levelDirecteion = end - new Vector2(start.x, end.y);
            Vector2 up = new Vector2(0.0f, 1.0f);
            //if (end.y > start.y) up = -up;
            Vector2 result = start + t * travelDirection;
            result += ((-parabolicT * parabolicT + 1) * height) * up;
            return result;
        }

    }


    //Rotiert die Schnipsel random
    public void changeMyRotation(GameObject current)
    {
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 20);
        current.transform.eulerAngles = current.transform.eulerAngles + euler;
    }


    //färbt die Schnipsel random um
    public void giveMeRandomColor(GameObject konfettiPiece)
    {
        konfettiPiece.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    //Verschiebt die Vorher in die szene gesetzen Zielpunkte random
    public void moveTargetsRandomAround()
    {
        foreach (GameObject currentSpot in lootTargets)
        {
            currentSpot.transform.position = new Vector3(currentSpot.transform.position.x + Random.RandomRange(-2, 2), currentSpot.transform.position.y + Random.RandomRange(-2, 2), 0);
        }

    }

}

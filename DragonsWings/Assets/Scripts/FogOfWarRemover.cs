using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWarRemover : MonoBehaviour
{

    public Tilemap fogOfWarMap;

    public int radius;

    private Vector2Int oldPosition = new Vector2Int (100000,10000);

	// Use this for initialization
	void Start ()
    {
        firstCircle();

    }
	
	// Update is called once per frame
	void Update ()
    {

        Vector2Int position = new Vector2Int ((int) transform.position.x, (int) transform.position.y);

        if (!(oldPosition == position))

        {
            foreach (Vector2Int current in calcCircleMatrix(position, radius))
            {
                fogOfWarMap.SetTile(new Vector3Int(current.x, current.y, 0), null);
            }

            oldPosition = position;
        }
        
	}


    public List<Vector2Int> calcCircleMatrix(Vector2Int middle,int radius)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        int x0 = middle.x;
        int y0 = middle.y;


        int x = radius - 1;
        int y = 0;
        int dx = 1;
        int dy = 1;
        int err = dx - (radius << 1);

        while (x >= y)
        {
            result.Add(new Vector2Int(x0 + x, y0 + y));

            result.Add(new Vector2Int(x0 + y, y0 + x));
            result.Add(new Vector2Int(x0 - y, y0 + x));
            result.Add(new Vector2Int(x0 - x, y0 + y));
            result.Add(new Vector2Int(x0 - x, y0 - y));
            result.Add(new Vector2Int(x0 - y, y0 - x));
            result.Add(new Vector2Int(x0 + y, y0 - x));
            result.Add(new Vector2Int(x0 + x, y0 - y));

            if (err <= 0)
            {
                y++;
                err += dy;
                dy += 2;
            }

            if (err > 0)
            {
                x--;
                dx += 2;
                err += dx - (radius << 1);
            }
        }


        return result;
    }

    public void firstCircle()
    {
        Vector2Int position = new Vector2Int((int) transform.position.x, (int) transform.position.y);

        for (int i = 0; i <= radius; i++)
        { 
        foreach (Vector2Int current in calcCircleMatrix(position, i))
        {
            fogOfWarMap.SetTile(new Vector3Int(current.x, current.y, 0), null);
        }

        oldPosition = position;

    }

    }




}

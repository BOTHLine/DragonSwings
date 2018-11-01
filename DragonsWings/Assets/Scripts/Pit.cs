using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    public Vector2Reference playerPosition;

    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {

        if (Physics2D.OverlapPoint(playerPosition, LayerList.FallCheck.LayerMask) == boxCollider2D)
        {
            UnityEditor.Handles.color = new Color(255f, 0f, 0f, 0.4f);
            UnityEditor.Handles.DrawSolidDisc(playerPosition.Value, Vector3.forward, 0.1f);
        }
    }
}
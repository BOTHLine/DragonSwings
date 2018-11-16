using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    private Renderer myRenderer;

    [SerializeField] private bool runOnlyOnce = false;
    [SerializeField] private int offset = 0;

    private int positionMultiplier = 100;

    private float timer;
    private float timerMax = 0.1f;

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = timerMax;
            myRenderer.sortingOrder = (int)(transform.position.y * -positionMultiplier) + offset;
            if (runOnlyOnce)
                Destroy(this);
        }
    }
}
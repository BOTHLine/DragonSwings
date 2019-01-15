using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour
{
    private Renderer _Renderer;

    private Color _OriginalColor;

    public FloatReference _BlinkTime;
    public ColorReference _BlinkColor;

    private void Awake()
    {
        _Renderer = transform.parent.GetComponent<Renderer>();
        _OriginalColor = _Renderer.material.color;
    }

    public void StartBlink()
    {
        IEnumerator BlinkCoroutine = Blink(_BlinkTime.Value, _BlinkColor.Value);
        StartCoroutine(BlinkCoroutine);
    }

    IEnumerator Blink(float blinkTime, Color blinkColor)
    {
        _Renderer.material.color = blinkColor;
        yield return new WaitForSeconds(blinkTime);
        _Renderer.material.color = _OriginalColor;
    }

}
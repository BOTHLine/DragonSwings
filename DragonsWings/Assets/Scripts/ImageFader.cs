using UnityEngine;

public class ImageFader : MonoBehaviour
{
    public SpriteRenderer _SpriteRenderer;

    public float _TargetAlpha;
    public float _Time;

    public void StartFade()
    {
        System.Collections.IEnumerator fadeCoroutine = Fade();
        StartCoroutine(fadeCoroutine);
    }

    private System.Collections.IEnumerator Fade()
    {
        _TargetAlpha = Mathf.Clamp01(_TargetAlpha);

        float startAlpha = _SpriteRenderer.color.a;
        float alphaDifference = _TargetAlpha - startAlpha;
        Color currentColor = _SpriteRenderer.color;
        while (currentColor.a != _TargetAlpha)
        {
            float alphaStep = alphaDifference / (Time.deltaTime * _Time);
            currentColor.a += alphaStep;
            _SpriteRenderer.color = currentColor;
            yield return new WaitForEndOfFrame();
        }
    }
}
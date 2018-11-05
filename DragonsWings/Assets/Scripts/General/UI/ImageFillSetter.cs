using UnityEngine;

public class ImageFillSetter : MonoBehaviour
{
    public FloatReference variable;
    public FloatReference max;

    public UnityEngine.UI.Image image;

    private void Update()
    {
        image.fillAmount = Mathf.Clamp01(variable.Value / max.Value);
    }
}
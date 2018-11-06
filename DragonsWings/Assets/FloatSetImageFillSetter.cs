using UnityEngine;

public class FloatSetImageFillSetter : MonoBehaviour
{
    public Transform identifier;
    public FloatSet variable;
    public FloatReference max;

    public UnityEngine.UI.Image image;

    private void Update()
    {
        image.fillAmount = Mathf.Clamp01(variable.Get(identifier) / max.Value);
    }
}
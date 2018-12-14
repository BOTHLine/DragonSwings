using UnityEngine;

public class FloatSetImageFillSetter : MonoBehaviour
{
    public FloatReference _Max;
    public FloatReference _Actual;

    public UnityEngine.UI.Image _Image;

    private void Update()
    {
        _Image.fillAmount = Mathf.Clamp01(_Actual.Value / _Max.Value);
    }
}
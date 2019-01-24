using UnityEngine;

public class ImageFillSetter : MonoBehaviour
{
    public FloatReference _ValueMax;
    public FloatReference _ValueCurrent;

    public UnityEngine.UI.Image image;

    /*
    private void Awake()
    {
        _ValueCurrent.Subscribe(UpdateHealthBar);
    }

    public void UpdateHealthBar(float newValueCurrent)
    { image.fillAmount = Mathf.Clamp01(newValueCurrent / _ValueMax.Value); Debug.Log("New Health Bar Fill Value: " + image.fillAmount); }
    */

    private void Update()
    {
        image.fillAmount = Mathf.Clamp01(_ValueCurrent.Value / _ValueMax.Value);
    }
}
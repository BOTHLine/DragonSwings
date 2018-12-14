using UnityEngine;

public class GameObjectToggler : MonoBehaviour
{
    public GameObject _TargetObject;

    public void Toggle()
    { _TargetObject.SetActive(!_TargetObject.activeSelf); }
}
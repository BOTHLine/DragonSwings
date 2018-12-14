using UnityEngine;

public class SkipPoint : MonoBehaviour
{
    public GameObject _TargetObject;

    public void Skip()
    {
        _TargetObject.transform.position = transform.position;
    }
}
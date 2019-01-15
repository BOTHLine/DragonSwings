using UnityEngine;

public class LookForward : MonoBehaviour
{
    public Rigidbody _Rigidbody;
    public float _CorrectionValue;

    private void Awake()
    {
        _Rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        if (_Rigidbody.velocity.Equals(Vector3.zero))
            return;

        transform.parent.rotation = Utils.GetLookAtRotation(_Rigidbody.velocity, _CorrectionValue);
    }
}
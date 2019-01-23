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
        if (_Rigidbody.velocity.sqrMagnitude <= 0.001f)
            return;

        Vector3 newRotation = transform.parent.localEulerAngles;
        newRotation.z = Vector2.SignedAngle(Vector2.down, new Vector2(_Rigidbody.velocity.x, _Rigidbody.velocity.z));
        transform.parent.localEulerAngles = newRotation;
    }
}
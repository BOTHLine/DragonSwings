using UnityEngine;

public class LookForward : MonoBehaviour
{
    public Rigidbody2D _Rigidbody2D;
    public float _CorrectionValue;

    private void Awake()
    {
        _Rigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_Rigidbody2D.velocity.Equals(Vector2.zero))
            return;

        transform.parent.rotation = Utils.GetLookAtRotation(_Rigidbody2D.velocity, _CorrectionValue);
    }
}
using UnityEngine;

public class ThrowAutoAim : MonoBehaviour
{
    private struct ClosestThrowResponder
    {
        public ThrowResponder throwResponder;
        public float distance;
    }

    // References
    [SerializeField] private FloatReference _AimRange;
    [SerializeField] private Vector2Reference _AimRawDirection;
    [SerializeField] private Vector2Reference _AimRawPosition;

    [SerializeField] private FloatReference _AimAutoRadius;

    [SerializeField] private Vector2Reference _AimAutoDirection;
    [SerializeField] private Vector2Reference _AimAutoPosition;

    // Variables
    [SerializeField] private Color NoAimColor;
    [SerializeField] private Color NoTargetColor;
    [SerializeField] private Color TargetFoundColor;
    [SerializeField] private Color TargetLineColor;

    // Mono Behaviour

    private void Update()
    {
        _AimRawPosition.Value = (Vector2)transform.position + (_AimRawDirection.Value * _AimRange);

        ThrowResponder throwResponder = FindClosestThrowResponder();
        if (throwResponder != null)
        {
            _AimAutoDirection.Value = (throwResponder.transform.position - transform.position).normalized;
            _AimAutoPosition.Value = throwResponder.transform.position;
        }
        else
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _AimRawDirection, _AimRange, LayerList.PlayerProjectile.LayerMask);
            _AimAutoDirection.Value = _AimRawDirection;
            _AimAutoPosition.Value = (Vector2)transform.position + _AimRawDirection.Value.normalized * raycastHit2D.distance;
        }
    }

    private ThrowResponder FindClosestThrowResponder()
    {
        if ((_AimRawDirection).Equals(Vector2.zero)) { return null; }

        ClosestThrowResponder closestThrowResponder = new ClosestThrowResponder();

        Vector2 closestTarget = _AimRawPosition;
        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll((Vector2)transform.position + (_AimRawDirection.Value.normalized * _AimAutoRadius), _AimAutoRadius, _AimRawDirection, _AimRange, LayerList.PlayerProjectile.LayerMask);
        for (int i = 0; i < raycastHit2Ds.Length; i++)
        {
            ThrowResponder throwResponder = raycastHit2Ds[i].collider.GetComponentInSiblings<ThrowResponder>();
            if (throwResponder == null || !throwResponder._AutoAim) { continue; }

            if (((Vector2)transform.position - (Vector2)throwResponder.transform.position).sqrMagnitude > _AimRange * _AimRange) { continue; }

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, throwResponder.transform.position - transform.position, _AimRange, LayerList.PlayerProjectile.LayerMask);
            if (raycastHit2D.collider != raycastHit2Ds[i].collider) { continue; }

            float newDistance = Vector2.Distance(transform.position, throwResponder.transform.position);
            if (closestThrowResponder.throwResponder != null && closestThrowResponder.distance < newDistance) { continue; }

            closestThrowResponder.throwResponder = throwResponder;
            closestThrowResponder.distance = newDistance;
        }
        return closestThrowResponder.throwResponder;
    }
}
using UnityEngine;

public class ThrowAutoAim : MonoBehaviour
{
    private struct ThrowResponderDistance
    {
        public ThrowResponder throwResponder;
        public float distance;
        public float value { get { return throwResponder._AutoAimPriority - distance; } }
    }

    // References
    [SerializeField] private FloatReference _AimRange;

    [SerializeField] private Vector3ComplexReference _AimRaw;
    [SerializeField] private Vector3ComplexReference _AimAuto;

    [SerializeField] private FloatReference _AimAutoRadius;

    [SerializeField] private BoolReference _UseAutoAim;

    // Variables
    [SerializeField] private Color NoAimColor;
    [SerializeField] private Color NoTargetColor;
    [SerializeField] private Color TargetFoundColor;
    [SerializeField] private Color TargetLineColor;

    // Mono Behaviour

    private void Update()
    {
        Vector3Complex aimRaw = _AimRaw.Value;
        aimRaw.Magnitude = _AimRange.Value;
        _AimRaw.Value = aimRaw;

        Vector3Complex aimAuto = new Vector3Complex(_AimRaw.Value);

        ThrowResponder hookResponder = null;
        if (_UseAutoAim.Value) { hookResponder = FindClosestThrowResponder(); }

        if (hookResponder != null)
        {
            aimAuto.EndPoint = hookResponder.transform.position;
        }
        else
        {
            RaycastHit raycastHit;
            Physics.Raycast(transform.position, aimRaw.Direction, out raycastHit, aimRaw.Magnitude, LayerList.PlayerProjectile.LayerMask);
            aimAuto.Magnitude = (raycastHit.collider ? raycastHit.distance : _AimRange.Value);

            // RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, aimRaw.Direction, aimRaw.Magnitude, LayerList.PlayerProjectile.LayerMask);
            // aimAuto.Magnitude = (raycastHit2D.collider ? raycastHit2D.distance : _AimRange.Value);
        }

        _AimAuto.Value = aimAuto;
    }

    private ThrowResponder FindClosestThrowResponder()
    {
        if ((_AimRaw.Value.Direction).Equals(Vector3.zero)) { return null; }

        ThrowResponderDistance closestThrowResponder = new ThrowResponderDistance();

        Vector3 closestTarget = _AimRaw.Value.EndPoint;
        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position + (_AimRaw.Value.Direction * _AimAutoRadius.Value), _AimAutoRadius.Value, _AimRaw.Value.Direction, _AimRange.Value, LayerList.PlayerProjectile.LayerMask);
        // RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll((Vector2)transform.position + (_AimRaw.Value.Direction * _AimAutoRadius.Value), _AimAutoRadius.Value, _AimRaw.Value.Direction, _AimRange.Value, LayerList.PlayerProjectile.LayerMask);
        for (int i = 0; i < raycastHits.Length; i++)
        {
            ThrowResponder throwResponder = raycastHits[i].collider.GetComponentInSiblings<ThrowResponder>();
            if (throwResponder == null || !throwResponder._AutoAim) { continue; }

            if (((Vector2)transform.position - (Vector2)throwResponder.transform.position).sqrMagnitude > _AimRange * _AimRange) { continue; }

            if (Physics.Raycast(transform.position, throwResponder.transform.position - transform.position, _AimRange.Value, LayerList.PlayerAttack.LayerMask)) { continue; }
            // RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, throwResponder.transform.position - transform.position, _AimRange.Value, LayerList.PlayerProjectile.LayerMask);
            //if (raycastHit2D.collider != raycastHits[i].collider) { continue; }

            float newDistance = Vector3.Distance(transform.position, throwResponder.transform.position);
            ThrowResponderDistance newThrowResponderDistance = new ThrowResponderDistance();
            newThrowResponderDistance.throwResponder = throwResponder;
            newThrowResponderDistance.distance = newDistance;

            if (closestThrowResponder.throwResponder != null && closestThrowResponder.value < newThrowResponderDistance.value) { continue; }

            closestThrowResponder.throwResponder = throwResponder;
            closestThrowResponder.distance = newDistance;
        }
        return closestThrowResponder.throwResponder;
    }
}
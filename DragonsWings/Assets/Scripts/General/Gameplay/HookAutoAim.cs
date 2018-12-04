using UnityEngine;

public class HookAutoAim : MonoBehaviour
{
    private struct ClosestHookResponder
    {
        public HookResponder hookResponder;
        public float distance;
    }

    // References
    [SerializeField] private FloatReference _AimRange;

    [SerializeField] private Vector2ComplexReference _AimRaw;
    [SerializeField] private Vector2ComplexReference _AimAuto;

    [SerializeField] private Vector2Reference _AimRawDirection;
    [SerializeField] private Vector2Reference _AimRawPosition;

    [SerializeField] private FloatReference _AimAutoRadius;

    [SerializeField] private Vector2Reference _AimAutoDirection;
    [SerializeField] private Vector2Reference _AimAutoPosition;

    [SerializeField] private BoolReference _UseAutoAim;

    // Variables
    [SerializeField] private Color NoAimColor;
    [SerializeField] private Color NoTargetColor;
    [SerializeField] private Color TargetFoundColor;
    [SerializeField] private Color TargetLineColor;

    // Mono Behaviour

    private void Update()
    {
        /*
        Vector2Complex aimRaw = _AimRaw;
        aimRaw.Magnitude = _AimRange;
        _AimRaw.Value = aimRaw;

        Vector2Complex aimAuto = new Vector2Complex(_AimRaw);
        HookResponder hookResponder = null;
        if (_UseAutoAim.Value) { hookResponder = FindClosestHookResponder(); }
        if (hookResponder != null)
        {
            aimAuto.EndPoint = hookResponder.transform.position;
        }
        else
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, aimRaw.Direction, aimRaw.Magnitude, LayerList.PlayerProjectile.LayerMask);
            aimAuto.Magnitude = (raycastHit2D.collider ? raycastHit2D.distance : _AimRange);
        }

        _AimAuto.Value = aimAuto;
        */

        _AimRawPosition.Value = (Vector2)transform.position + (_AimRawDirection.Value * _AimRange);

        HookResponder hookResponder = null;
        if (_UseAutoAim.Value) { hookResponder = FindClosestHookResponder(); }

        _AimAutoDirection.Value = hookResponder != null ? (Vector2)(hookResponder.transform.position - transform.position).normalized : _AimRawDirection;
        _AimAutoPosition.Value = hookResponder != null ? (Vector2)hookResponder.transform.position : _AimRawPosition;
    }

    private HookResponder FindClosestHookResponder()
    {
        if ((_AimRawDirection.Value).Equals(Vector2.zero)) { return null; }

        ClosestHookResponder closestHookResponder = new ClosestHookResponder();

        Vector2 closestTarget = _AimRawPosition;
        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll((Vector2)transform.position + (_AimRawDirection.Value.normalized * _AimAutoRadius), _AimAutoRadius, _AimRawDirection, _AimRange, LayerList.PlayerProjectile.LayerMask);
        for (int i = 0; i < raycastHit2Ds.Length; i++)
        {
            HookResponder hookResponder = raycastHit2Ds[i].collider.GetComponentInSiblings<HookResponder>();
            if (hookResponder == null || !hookResponder._AutoAim) { continue; }

            if (((Vector2)transform.position - (Vector2)hookResponder.transform.position).sqrMagnitude > _AimRange * _AimRange) { continue; }

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, hookResponder.transform.position - transform.position, _AimRange, LayerList.PlayerProjectile.LayerMask);
            if (raycastHit2D.collider != raycastHit2Ds[i].collider) { continue; }

            float newDistance = Vector2.Distance(transform.position, hookResponder.transform.position);
            if (closestHookResponder.hookResponder != null && closestHookResponder.distance < newDistance) { continue; }

            closestHookResponder.hookResponder = hookResponder;
            closestHookResponder.distance = newDistance;
        }
        return closestHookResponder.hookResponder;
    }
}
using UnityEngine;

public class HookAutoAim : MonoBehaviour
{
    private struct HookResponderDistance
    {
        public HookResponder hookResponder;
        public float distance;
        public float value { get { return hookResponder._AutoAimPriority - distance; } }
    }

    // References
    [SerializeField] private FloatReference _AimRange;

    [SerializeField] private Vector3ComplexReference _AimRaw;
    [SerializeField] private Vector3ComplexReference _AimSmart;

    [SerializeField] private FloatReference _AimAutoRadius;

    [SerializeField] private BoolReference _UseAutoAim;

    [SerializeField] private FloatReference _HookPushboxRadius;

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

        Vector3Complex aimSmart = new Vector3Complex(_AimRaw.Value);

        HookResponder hookResponder = null;
        if (_UseAutoAim.Value) { hookResponder = FindClosestHookResponder(); }

        if (hookResponder != null)
        {
            aimSmart.EndPoint = hookResponder.transform.position;
        }
        else
        {
            RaycastHit raycastHit;
            Physics.SphereCast(transform.position, _HookPushboxRadius.Value, aimRaw.Direction, out raycastHit, aimRaw.Magnitude, Layer.PlayerProjectile.GetLayerMask());
            aimSmart.Magnitude = (raycastHit.collider ? raycastHit.distance : _AimRange.Value);

            // RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, _HookPushboxRadius.Value, aimRaw.Direction, aimRaw.Magnitude, LayerList.PlayerProjectile.LayerMask);
            // RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, aimRaw.Direction, aimRaw.Magnitude, LayerList.PlayerProjectile.LayerMask);
            // aimAuto.Magnitude = (raycastHit2D.collider ? raycastHit2D.distance : _AimRange.Value);
        }

        _AimSmart.Value = aimSmart;
    }

    private HookResponder FindClosestHookResponder()
    {
        if ((_AimRaw.Value.Direction).Equals(Vector3.zero)) { return null; }

        HookResponderDistance closestHookResponder = new HookResponderDistance();

        Vector3 closestTarget = _AimRaw.Value.EndPoint;
        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position + (_AimRaw.Value.Direction * _AimAutoRadius.Value), _AimAutoRadius.Value, _AimRaw.Value.Direction, _AimRange.Value, Layer.PlayerProjectile.GetLayerMask());
        // RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll((Vector2)transform.position + (_AimRaw.Value.Direction * _AimAutoRadius.Value), _AimAutoRadius.Value, _AimRaw.Value.Direction, _AimRange.Value, LayerList.PlayerProjectile.LayerMask);
        for (int i = 0; i < raycastHits.Length; i++)
        {
            HookResponder hookResponder = raycastHits[i].collider.GetComponentInSiblings<HookResponder>();
            if (hookResponder == null || !hookResponder._AutoAim) { continue; }

            if ((transform.position - hookResponder.transform.position).sqrMagnitude > _AimRange * _AimRange) { continue; }

            RaycastHit raycastHit;
            if (!Physics.SphereCast(transform.position, _HookPushboxRadius.Value, hookResponder.transform.position - transform.position, out raycastHit, _AimRange.Value, Layer.PlayerProjectile.GetLayerMask())) { continue; }

            // RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, _HookPushboxRadius.Value, hookResponder.transform.position - transform.position, _AimRange.Value, LayerList.PlayerProjectile.LayerMask);
            // RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, hookResponder.transform.position - transform.position, _AimRange.Value, LayerList.PlayerProjectile.LayerMask);
            // if (raycastHit2D.collider != raycastHits[i].collider) { continue; }

            float newDistance = Vector3.Distance(transform.position, hookResponder.transform.position);
            HookResponderDistance newHookResponderDistance = new HookResponderDistance();
            newHookResponderDistance.hookResponder = hookResponder;
            newHookResponderDistance.distance = newDistance;

            if (closestHookResponder.hookResponder != null && closestHookResponder.value < newHookResponderDistance.value) { continue; }

            closestHookResponder.hookResponder = hookResponder;
            closestHookResponder.distance = newDistance;
        }
        return closestHookResponder.hookResponder;
    }
}
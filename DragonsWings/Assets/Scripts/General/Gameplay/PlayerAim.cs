using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private struct ClosestHookResponder
    {
        public HookResponder hookResponder;
        public float distance;
    }

    // Components
    private CircleCollider2D _Crosshair;

    // References
    [SerializeField] private FloatReference _AimRange;
    [SerializeField] private Vector2Reference _AimRawDirection;
    [SerializeField] private Vector2Reference _AimRawPosition;

    [SerializeField] private FloatReference _AimAutoRadius;

    [SerializeField] private Vector2Reference _AimAutoPosition;

    // Variables
    [SerializeField] private Color NoAimColor;
    [SerializeField] private Color NoTargetColor;
    [SerializeField] private Color TargetFoundColor;
    [SerializeField] private Color TargetLineColor;

    // Mono Behaviour
    private void Awake()
    {
        _Crosshair = GetComponentInChildren<CircleCollider2D>();
    }

    private void Update()
    {
        _Crosshair.transform.localPosition = _AimRawDirection.Value * _AimRange;
        _AimRawPosition.Value = _Crosshair.transform.position;

        HookResponder hookResponder = FindClosestHookResponder();
        if (hookResponder == null) { _AimAutoPosition.Value = _AimRawPosition.Value; }
        else { _AimAutoPosition.Value = hookResponder.transform.position; }
    }

    private HookResponder FindClosestHookResponder()
    {
        _AimAutoPosition.Value = transform.position;

        if (((Vector2)_Crosshair.transform.localPosition).Equals(Vector2.zero)) { return null; }

        ClosestHookResponder closestHookResponder = new ClosestHookResponder();

        Vector2 closestTarget = _Crosshair.transform.position;
        RaycastHit2D[] circlecastHit2D = Physics2D.CircleCastAll((Vector2)transform.position + (_AimRawDirection.Value.normalized * _AimAutoRadius), _AimAutoRadius, _AimRawDirection, _AimRange, LayerList.CreateLayerMask(gameObject.layer));
        for (int i = 0; i < circlecastHit2D.Length; i++)
        {
            HookResponder hookResponder = circlecastHit2D[i].collider.GetComponent<HookResponder>();
            if (hookResponder == null || !hookResponder._AutoAim) { continue; }

            if (((Vector2)transform.position - (Vector2)hookResponder.transform.position).sqrMagnitude > _AimRange * _AimRange) { continue; }

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, hookResponder.transform.position - transform.position, _AimRange, LayerList.CreateLayerMask(gameObject.layer));
            if (raycastHit2D.collider != circlecastHit2D[i].collider) { continue; }

            ColliderDistance2D colliderDistance2D = _Crosshair.Distance(circlecastHit2D[i].collider);
            if (colliderDistance2D.isValid && (closestHookResponder.hookResponder == null ^ colliderDistance2D.distance < closestHookResponder.distance))
            {
                closestHookResponder.hookResponder = hookResponder;
                closestHookResponder.distance = colliderDistance2D.distance;
            }
        }
        return closestHookResponder.hookResponder;
    }
}
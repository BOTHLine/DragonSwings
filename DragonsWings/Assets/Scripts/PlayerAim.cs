using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;

    public FloatReference aimRange;
    public Vector2Reference aimDirection;
    public Vector2Reference aimPosition;

    public FloatReference aimHelpRadius;

    private bool hasTarget;
    public Vector2Reference autoAimPosition;

    [SerializeField] private Color NoAimColor;
    [SerializeField] private Color NoTargetColor;
    [SerializeField] private Color TargetFoundColor;
    [SerializeField] private Color TargetLineColor;

    private ColliderDistance2D closestColliderDistance2D;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        transform.localPosition = aimDirection.Value * aimRange;
        aimPosition.Variable.Value = transform.position;

        FindClosestHookable();
    }

    private void FindClosestHookable()
    {
        autoAimPosition.Variable.Value = transform.position;
        hasTarget = false;

        if ((Vector2)transform.localPosition == Vector2.zero)
            return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, aimHelpRadius, LayerList.Hook.LayerMask);
        if (colliders.Length >= 1)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Aimable aimable = colliders[i].GetComponent<Aimable>();
                if (aimable != null)
                {
                    if (!hasTarget)
                    {
                        closestColliderDistance2D = circleCollider2D.Distance(colliders[i]);
                        if (closestColliderDistance2D.isValid)
                        {
                            autoAimPosition.Variable.Value = closestColliderDistance2D.pointB;
                            hasTarget = true;
                        }
                    }
                    else
                    {
                        ColliderDistance2D newColliderDistance2D = circleCollider2D.Distance(colliders[i]);
                        if (newColliderDistance2D.isValid && newColliderDistance2D.distance < closestColliderDistance2D.distance)
                        {
                            closestColliderDistance2D = newColliderDistance2D;
                            autoAimPosition.Variable.Value = closestColliderDistance2D.pointB;
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if ((Vector2)transform.localPosition == Vector2.zero)
            UnityEditor.Handles.color = NoAimColor;
        else if (!hasTarget)
            UnityEditor.Handles.color = NoTargetColor;
        else
        {
            UnityEditor.Handles.color = TargetLineColor;
            UnityEditor.Handles.DrawLine(aimPosition.Value, autoAimPosition.Value);
            UnityEditor.Handles.color = TargetFoundColor;
        }
        UnityEditor.Handles.DrawSolidDisc(aimPosition.Value, Vector3.forward, aimHelpRadius);
    }
}
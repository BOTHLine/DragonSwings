using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;

    public Vector2Reference _SourcePosition;
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

        Vector2 closestTarget = transform.position;
        RaycastHit2D[] hit2Ds = Physics2D.CircleCastAll(_SourcePosition + (aimDirection.Value.normalized * aimHelpRadius), aimHelpRadius, aimDirection, aimRange, LayerList.PlayerProjectile.LayerMask);
        for (int i = 0; i < hit2Ds.Length; i++)
        {
            HurtBox hurtBox = hit2Ds[i].collider.GetComponent<HurtBox>();
            if (hurtBox == null) { continue; }

            if (Vector2.Distance(_SourcePosition, hurtBox.transform.position) > aimRange) { continue; }

            RaycastHit2D hit2D = Physics2D.Raycast(_SourcePosition, (Vector2)hurtBox.transform.position - _SourcePosition, aimRange, LayerList.PlayerProjectile.LayerMask);
            if (hit2D.collider != hit2Ds[i].collider) { continue; }

            if (!hasTarget)
            {
                closestColliderDistance2D = circleCollider2D.Distance(hit2Ds[i].collider);
                if (closestColliderDistance2D.isValid)
                {
                    autoAimPosition.Variable.Value = hit2Ds[i].collider.transform.position;
                    hasTarget = true;
                }
            }
            else
            {
                ColliderDistance2D newColliderDistance2D = circleCollider2D.Distance(hit2Ds[i].collider);
                if (newColliderDistance2D.isValid && newColliderDistance2D.distance < closestColliderDistance2D.distance)
                {
                    closestColliderDistance2D = newColliderDistance2D;
                    autoAimPosition.Variable.Value = hit2Ds[i].collider.transform.position;
                }
            }
        }
        if (!hasTarget)
        {
            for (int i = 0; i < hit2Ds.Length; i++)
            {
                ThrowMyBox throwMyBox = hit2Ds[i].collider.GetComponent<ThrowMyBox>();
                if (throwMyBox == null) { continue; }

                if (Vector2.Distance(_SourcePosition, throwMyBox.transform.position) > aimRange) { continue; }

                RaycastHit2D hit2D = Physics2D.Raycast(_SourcePosition, (Vector2)throwMyBox.transform.position - _SourcePosition, aimRange, LayerList.PlayerProjectile.LayerMask);
                if (hit2D.collider != hit2Ds[i].collider) { continue; }

                if (!hasTarget)
                {
                    closestColliderDistance2D = circleCollider2D.Distance(hit2Ds[i].collider);
                    if (closestColliderDistance2D.isValid)
                    {
                        autoAimPosition.Variable.Value = hit2Ds[i].collider.transform.position;
                        hasTarget = true;
                    }
                }
                else
                {
                    ColliderDistance2D newColliderDistance2D = circleCollider2D.Distance(hit2Ds[i].collider);
                    if (newColliderDistance2D.isValid && newColliderDistance2D.distance < closestColliderDistance2D.distance)
                    {
                        closestColliderDistance2D = newColliderDistance2D;
                        autoAimPosition.Variable.Value = hit2Ds[i].collider.transform.position;
                    }
                }
            }
        }

        /*
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, aimHelpRadius, LayerList.PlayerProjectile.LayerMask);
        if (colliders.Length >= 1)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                // TODO HurtBox als Aimable nutzen oder nicht? Eigentlich will man nur HurtBoxen anvisieren können? Oder HurtBox muss Aimable implementieren, anstatt die Gegner selbst. Dann hat aber auch Spieler eine HurtBox mit Aimable drauf

                HurtBox hurtBox = colliders[i].GetComponentInSiblings<HurtBox>();
                if (hurtBox == null) { continue; }

                //    HookInteraction hookInteraction = colliders[i].GetComponentInSiblings<HookInteraction>();
                //    if (hookInteraction == null || !hookInteraction._AutoAim) { continue; }

                if (Vector2.Distance(_SourcePosition, colliders[i].transform.position) > aimRange) { continue; }

                RaycastHit2D hit2D = Physics2D.Raycast(_SourcePosition, (Vector2)colliders[i].transform.position - _SourcePosition, aimRange, LayerList.PlayerProjectile.LayerMask);
                if (hit2D.collider != colliders[i]) { continue; }

                if (!hasTarget)
                {
                    closestColliderDistance2D = circleCollider2D.Distance(colliders[i]);
                    if (closestColliderDistance2D.isValid)
                    {
                        autoAimPosition.Variable.Value = colliders[i].transform.position;
                        hasTarget = true;
                    }
                }
                else
                {
                    ColliderDistance2D newColliderDistance2D = circleCollider2D.Distance(colliders[i]);
                    if (newColliderDistance2D.isValid && newColliderDistance2D.distance < closestColliderDistance2D.distance)
                    {
                        closestColliderDistance2D = newColliderDistance2D;
                        autoAimPosition.Variable.Value = colliders[i].transform.position;
                    }
                }
            }
        }*/
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
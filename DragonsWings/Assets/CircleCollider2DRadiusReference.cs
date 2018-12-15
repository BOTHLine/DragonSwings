using UnityEngine;

public class CircleCollider2DRadiusReference : MonoBehaviour
{
    public FloatReference _Collider2DRadius;

    private void Awake()
    { _Collider2DRadius.Value = this.GetComponentInSiblings<PushBox>().GetComponent<CircleCollider2D>().radius; }
}
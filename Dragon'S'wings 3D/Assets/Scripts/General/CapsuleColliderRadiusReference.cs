using UnityEngine;

public class CapsuleColliderRadiusReference : MonoBehaviour
{
    public FloatReference _ColliderRadius;

    private void Awake()
    { _ColliderRadius.Value = this.GetComponentInSiblings<PushBox>().GetComponent<CapsuleCollider>().radius; }
}
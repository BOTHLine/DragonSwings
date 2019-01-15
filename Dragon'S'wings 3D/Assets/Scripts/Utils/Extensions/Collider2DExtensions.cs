using UnityEngine;

public static class Collider2DExtensions
{
    public static int OverlapColliderWithOwnLayerMask(this Collider2D collider2D, Collider2D[] collider2Ds)
    {
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(LayerList.CreateLayerMask(collider2D.gameObject.layer));

        return collider2D.OverlapCollider(contactFilter2D, collider2Ds);
    }
}
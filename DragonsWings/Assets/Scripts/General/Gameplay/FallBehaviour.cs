using UnityEngine;

public class FallBehaviour : MonoBehaviour
{
    // Components
    private CircleCollider2D _CircleCollider2D;

    // Variables
    private int _LayerMask;

    // Events
    public GameEvent _OnPlayerLand;

    // Methods

    private void Awake()
    {
        _CircleCollider2D = GetComponentInParent<CircleCollider2D>();
    }

    private void CreateLayerMask()
    {
        _LayerMask = 0;
        int layer = gameObject.layer;
        for (int i = 0; i < 32; i++)
        {
            if (!Physics2D.GetIgnoreLayerCollision(layer, i))
            {
                _LayerMask = _LayerMask | 1 << i;
            }
        }
    }

    public void CheckFall()
    {
        Debug.Log("Hi");
        _OnPlayerLand.Raise();
        Collider2D collider = Physics2D.OverlapCircle(_CircleCollider2D.transform.position, _CircleCollider2D.radius, _LayerMask);
        if (collider != null)
        {
            _OnPlayerLand.Raise();
        }
    }
}
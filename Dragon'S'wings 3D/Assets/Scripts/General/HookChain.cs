using UnityEngine;

public class HookChain : MonoBehaviour
{
    [HideInInspector] public Transform _ChainFromPosition;
    [HideInInspector] public Transform _ChainToPosition;

    private SpriteRenderer _SpriteRenderer;

    private void Awake()
    {
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _SpriteRenderer.enabled = false;

        _ChainFromPosition = transform.parent.parent;
        _ChainToPosition = transform.parent;
    }

    private void Update()
    {
        if (_SpriteRenderer.enabled) { UpdateHookChain(); }
    }

    private void UpdateHookChain()
    {
        Vector3 newRotation = _SpriteRenderer.transform.localEulerAngles;
        newRotation.z = Vector2.SignedAngle(Vector2.down, _ChainFromPosition.position - _ChainToPosition.position);
        _SpriteRenderer.transform.localEulerAngles = newRotation;

        _SpriteRenderer.size = new Vector2(Vector3.Distance(_ChainFromPosition.position, _ChainToPosition.position) / transform.lossyScale.x, _SpriteRenderer.size.y);
    }

    public void SetActive(bool active)
    {
        UpdateHookChain();
        _SpriteRenderer.enabled = active;
    }
}
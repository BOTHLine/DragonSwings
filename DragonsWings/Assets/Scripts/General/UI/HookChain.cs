using UnityEngine;

public class HookChain : MonoBehaviour
{
    [HideInInspector] public Transform _ChainFromPosition;
    [HideInInspector] public Transform _ChainToPosition;

    private SpriteRenderer hookChain;

    private void Awake()
    {
        hookChain = GetComponent<SpriteRenderer>();
        hookChain.enabled = false;

        _ChainFromPosition = transform.parent.parent;
        _ChainToPosition = transform.parent;
    }

    private void Update()
    {
        if (hookChain.enabled) { UpdateHookChain(); }
    }

    private void UpdateHookChain()
    {
        hookChain.transform.rotation = Utils.GetLookAtRotation(_ChainToPosition.position, _ChainFromPosition.position, 180.0f);
        hookChain.size = new Vector2(Vector2.Distance(_ChainFromPosition.position, _ChainToPosition.position) / transform.lossyScale.x, hookChain.size.y);
    }

    public void SetActive(bool active)
    {
        UpdateHookChain();
        hookChain.enabled = active;
    }
}
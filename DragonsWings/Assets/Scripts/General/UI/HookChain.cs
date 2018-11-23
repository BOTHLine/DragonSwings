using UnityEngine;

public class HookChain : MonoBehaviour
{
    public Vector2Reference _ChainFromPosition;

    private SpriteRenderer hookChain;

    private void Awake()
    {
        hookChain = GetComponent<SpriteRenderer>();
        hookChain.enabled = false;
    }

    private void Update()
    {
        if (hookChain.enabled) { UpdateHookChain(); }
    }

    private void UpdateHookChain()
    {
        hookChain.size = new Vector2(Vector2.Distance(_ChainFromPosition, transform.position), hookChain.size.y);
        hookChain.transform.rotation = Utils.GetLookAtRotation(_ChainFromPosition, transform.position);
    }

    public void SetActive(bool active)
    {
        UpdateHookChain();
        hookChain.enabled = active;
    }
}
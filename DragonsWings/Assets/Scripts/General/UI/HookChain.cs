using UnityEngine;

public class HookChain : MonoBehaviour
{
    public Transform hookChainPrefab;

    private Transform parent;
    private SpriteRenderer hookChain;

    private void Awake()
    {
        parent = transform.parent;

        hookChain = Instantiate(hookChainPrefab).GetComponent<SpriteRenderer>();
        hookChain.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (hookChain.gameObject.activeInHierarchy)
        { UpdateHookChain(); }

    }

    private void UpdateHookChain()
    {
        hookChain.transform.position = parent.position;
        hookChain.size = new Vector2(Vector2.Distance(parent.transform.position, transform.position), hookChain.size.y);
        hookChain.transform.rotation = Utils.GetLookAtRotation(transform.position, parent.position);
    }

    public void SetActive(bool active)
    {
        UpdateHookChain();
        hookChain.gameObject.SetActive(active);
    }
}
using UnityEngine;

public class HookChain3D : MonoBehaviour
{
    [HideInInspector] public Transform _ChainFromPosition;
    [HideInInspector] public Transform _ChainToPosition;

    private Renderer _Renderer;

    private void Awake()
    {
        _Renderer = GetComponentInChildren<Renderer>();
        _Renderer.enabled = false;

        _ChainFromPosition = transform.parent.parent;
        _ChainToPosition = transform.parent;
    }

    private void Update()
    {
        if (_Renderer.enabled) { UpdateHookChain(); }
    }

    private void UpdateHookChain()
    {

        Vector3 vector = _ChainToPosition.position - _ChainFromPosition.position;
        transform.position = _ChainFromPosition.position + (vector / 2.0f);

        Vector3 newRotation = _Renderer.transform.localRotation.eulerAngles;
        newRotation.y = -Vector2.SignedAngle(Vector2.down, new Vector2(vector.x, vector.z).normalized);
        _Renderer.transform.localRotation = Quaternion.Euler(newRotation);

        Vector3 newScale = _Renderer.transform.localScale;
        newScale.y = vector.magnitude / 2.0f;
        _Renderer.transform.localScale = newScale;
    }

    public void SetActive(bool active)
    {
        UpdateHookChain();
        _Renderer.enabled = active;
    }
}
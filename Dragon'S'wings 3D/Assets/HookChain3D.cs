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

        // Debug.Log("Local Rotation Before: " + _Renderer.transform.localRotation.eulerAngles);
        // Debug.Log("Global Rotation Before: " + _Renderer.transform.rotation.eulerAngles);

        Vector3 newRotation = _Renderer.transform.localRotation.eulerAngles;
        newRotation.z = Vector2.SignedAngle(Vector2.down, new Vector2(vector.x, vector.z).normalized);
        newRotation.y = 0; // TODO WHHHHYYY?
        //   _Renderer.transform.eulerAngles = newRotation;
        _Renderer.transform.localRotation = Quaternion.Euler(newRotation); // TODO: Warum wird Z-Rotation auf Y addiert?

        // Debug.Log("Rotation should be: " + newRotation);
        // Debug.Log("Local Rotation After: " + _Renderer.transform.localRotation.eulerAngles);
        // Debug.Log("Global Rotation After: " + _Renderer.transform.rotation.eulerAngles);

        Vector3 newScale = _Renderer.transform.localScale;
        newScale.y = vector.magnitude / 2.0f;
        _Renderer.transform.localScale = newScale;

        /*
        Vector3 newRotation = _SpriteRenderer.transform.localEulerAngles;
        newRotation.z = Vector2.SignedAngle(Vector2.down, _ChainFromPosition.position - _ChainToPosition.position);
        _SpriteRenderer.transform.localEulerAngles = newRotation;

        _SpriteRenderer.size = new Vector2(Vector3.Distance(_ChainFromPosition.position, _ChainToPosition.position) / transform.lossyScale.x, _SpriteRenderer.size.y);
        */
    }

    public void SetActive(bool active)
    {
        UpdateHookChain();
        _Renderer.enabled = active;
    }
}
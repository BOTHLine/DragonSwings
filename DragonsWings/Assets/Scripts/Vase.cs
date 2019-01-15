using UnityEngine;

public class Vase : MonoBehaviour
{
    [HideInInspector] public PushBox _PushBox;
    [HideInInspector] public HurtBox _HurtBox;
    [HideInInspector] public ThrowResponder _ThrowResponder;
    [HideInInspector] public SpriteRenderer _SpriteRenderer;
    public Sprite[] _DestroyedSprites;

    private void Awake()
    {
        _PushBox = GetComponentInChildren<PushBox>();
        _HurtBox = GetComponentInChildren<HurtBox>();
        _ThrowResponder = GetComponentInChildren<ThrowResponder>();
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void DestroyVase()
    {
        _SpriteRenderer.sprite = _DestroyedSprites[(int)Mathf.Round(Random.value)];

        _PushBox?.gameObject.SetActive(false);
        _HurtBox?.gameObject.SetActive(false);
        _ThrowResponder?.gameObject.SetActive(false);
        _SpriteRenderer.sortingLayerName = "On Ground";
    }
}
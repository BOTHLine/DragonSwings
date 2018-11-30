using UnityEngine;

public class Box : MonoBehaviour
{
    // Components
    private SpriteRenderer _SpriteRenderer;
    private PushBox _PushBox;
    private HurtBox _HurtBox;
    private HookResponder _HookResponder;

    // Reference
    public FloatReference _Damage;
    public FloatReference _DamageRadius;

    // Variables
    public Sprite[] _Sprites;
    private int _CurrentSprite;

    private System.Collections.Generic.List<HurtBox> _AlreadyDamagedHurtBoxes = new System.Collections.Generic.List<HurtBox>();

    // Events
    // Mono Behaviour
    private void Awake()
    {
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _PushBox = GetComponentInChildren<PushBox>();
        _HurtBox = GetComponentInChildren<HurtBox>();
        _HookResponder = GetComponentInChildren<HookResponder>();
    }

    // Methods
    public void BoxLanded()
    {
        // TODO Anstatt HurtBoxes zu treffen, aus ThrowResponder auslagern. Die Objekte können dann selbst entscheiden, wie sie reagieren (Schaden nehmen, Hebel umlegen etc.)

        _AlreadyDamagedHurtBoxes.Clear();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, _DamageRadius, LayerList.CreateLayerMask(gameObject.layer));
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            HurtBox hurtBox = collider2Ds[i].GetComponentInSiblings<HurtBox>();
            if (hurtBox != null && !_AlreadyDamagedHurtBoxes.Contains(hurtBox))
            {
                hurtBox.Hurt(_Damage);
                _AlreadyDamagedHurtBoxes.Add(hurtBox);
                continue;
            }
        }
    }

    public void ChangeSprite()
    { if (++_CurrentSprite < _Sprites.Length) { _SpriteRenderer.sprite = _Sprites[_CurrentSprite]; } }

    public void DestroyBox()
    {
        _PushBox?.gameObject.SetActive(false);
        _HurtBox?.gameObject.SetActive(false);
        _HookResponder?.gameObject.SetActive(false);
    }
}
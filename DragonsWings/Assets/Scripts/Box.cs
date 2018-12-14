using UnityEngine;
using System.Collections.Generic;

public class Box : MonoBehaviour
{
    // Components
    public SpriteRenderer _SpriteRenderer;
    [HideInInspector] private PushBox _PushBox;
    [HideInInspector] private HurtBox _HurtBox;
    [HideInInspector] private HookResponder _HookResponder;

    public SpriteRenderer _LandedAnimationRenderer;


    // Reference
    public FloatReference _Damage;
    public FloatReference _DamageRadius;

    // Variables
    public Sprite[] _Sprites;
    private int _CurrentSprite;

    public Sprite[] _LandedAnimationSprites;

    private List<HurtBox> _AlreadyDamagedHurtBoxes = new List<HurtBox>();

    // Events
    // Mono Behaviour
    private void Awake()
    {
        _PushBox = GetComponentInChildren<PushBox>();
        _HurtBox = GetComponentInChildren<HurtBox>();
        _HookResponder = GetComponentInChildren<HookResponder>();
    }

    // Methods
    public void BoxLanded()
    {
        // TODO Anstatt HurtBoxes zu treffen, aus ThrowResponder auslagern. Die Objekte können dann selbst entscheiden, wie sie reagieren (Schaden nehmen, Hebel umlegen etc.)

        _AlreadyDamagedHurtBoxes.Clear();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, _DamageRadius.Value, LayerList.CreateLayerMask(gameObject.layer));
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            HurtBox hurtBox = collider2Ds[i].GetComponentInSiblings<HurtBox>();
            if (hurtBox != null && !_AlreadyDamagedHurtBoxes.Contains(hurtBox))
            {
                hurtBox.Hurt(_Damage.Value);
                _AlreadyDamagedHurtBoxes.Add(hurtBox);
                continue;
            }
        }

        System.Collections.IEnumerator PlayLandedAnimationCoroutine = PlayLandedAnimation(0.1f);
        StartCoroutine(PlayLandedAnimationCoroutine);
    }

    public void ChangeSprite()
    { if (++_CurrentSprite < _Sprites.Length) { _SpriteRenderer.sprite = _Sprites[_CurrentSprite]; } }

    public void DestroyBox()
    {
        _PushBox?.gameObject.SetActive(false);
        _HurtBox?.gameObject.SetActive(false);
        _HookResponder?.gameObject.SetActive(false);
    }

    private System.Collections.IEnumerator PlayLandedAnimation(float time)
    {
        _LandedAnimationRenderer.enabled = true;
        for (int i = 0; i < _LandedAnimationSprites.Length; i++)
        {
            _LandedAnimationRenderer.sprite = _LandedAnimationSprites[i];
            yield return new WaitForSeconds(time);
        }
        _LandedAnimationRenderer.enabled = false;
    }
}
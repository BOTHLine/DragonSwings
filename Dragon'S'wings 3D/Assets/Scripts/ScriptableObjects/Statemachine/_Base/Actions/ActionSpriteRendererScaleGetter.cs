using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/SpriteRenderer Scale Getter")]
public class ActionSpriteRendererScaleGetter : Action
{
    public Vector3Reference _Scale;

    public override void Act(StateController controller)
    { _Scale.Set(controller._SpriteRenderer.transform.localScale, controller.gameObject); }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/SpriteRenderer Scale Setter")]
public class ActionSpriteRendererScaleSetter : Action
{
    public Vector3Reference _Scale;

    public override void Act(StateController controller)
    { controller._SpriteRenderer.transform.localScale = _Scale.Get(controller.gameObject); }
}
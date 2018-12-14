using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/SpriteRenderer Color Setter")]
public class ActionSpriteRendererColorSetter : Action
{
    public ColorVariable _Color;

    public override void Act(StateController controller)
    { controller.spriteRenderer.color = _Color.Value; }
}
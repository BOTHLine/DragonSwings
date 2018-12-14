using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/SpriteRenderer Color Getter")]
public class ActionSpriteRendererColorGetter : Action
{
    public ColorVariable _Color;

    public override void Act(StateController controller)
    { _Color.Value = controller.spriteRenderer.color; }
}
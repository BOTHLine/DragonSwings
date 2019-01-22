using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/SpriteRenderer Color Changer")]
public class ActionSpriteRendererColorChanger : Action
{
    public FloatReference _ChangeValue;

    public override void Act(StateController controller)
    {
        Color currentColor = controller._SpriteRenderer.color;
        float colorFactor = _ChangeValue.Get(controller.gameObject) * Time.deltaTime;
        currentColor = new Color(currentColor.r - colorFactor, currentColor.g - colorFactor, currentColor.b - colorFactor);
        controller._SpriteRenderer.color = currentColor;
    }
}
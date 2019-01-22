using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/SpriteRenderer Scale Changer")]
public class ActionSpriteRendererScaleChanger : Action
{
    public FloatReference _ChangeValue;

    public override void Act(StateController controller)
    {
        Vector3 currentScale = controller._SpriteRenderer.transform.localScale;
        float scaleFactor = _ChangeValue.Get(controller.gameObject) * Time.deltaTime;
        currentScale = new Vector3(currentScale.x + scaleFactor, currentScale.y + scaleFactor);
        controller._SpriteRenderer.transform.localScale = currentScale;
    }
}
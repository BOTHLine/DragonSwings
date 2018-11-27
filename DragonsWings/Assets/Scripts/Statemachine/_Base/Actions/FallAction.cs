using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/Actions/Fall Action")]
public class FallAction : Action
{
    public float scaleDecrease;
    public float colorDecrease;

    private Vector3 originalScale;
    private Color originalColor;

    public override void Act(StateController controller)
    {
        Vector3 currentScale = controller.spriteRenderer.transform.localScale;
        float scaleFactor = scaleDecrease * Time.deltaTime;
        currentScale = new Vector3(currentScale.x - scaleFactor, currentScale.y - scaleFactor);
        controller.spriteRenderer.transform.localScale = currentScale;

        Color currentColor = controller.spriteRenderer.color;
        float colorFactor = colorDecrease * Time.deltaTime;
        currentColor = new Color(currentColor.r - colorFactor, currentColor.g - colorFactor, currentColor.b - colorFactor, currentColor.a);
        controller.spriteRenderer.color = currentColor;
    }

    public override void EnterState(StateController controller)
    {
        originalScale = controller.spriteRenderer.transform.localScale;
        originalColor = controller.spriteRenderer.color;

        controller.animator.SetBool("IsFalling", true);
    }

    public override void ExitState(StateController controller)
    {
        controller.animator.SetBool("IsFalling", false);

        controller.spriteRenderer.transform.localScale = originalScale;
        controller.spriteRenderer.color = originalColor;

        controller.spriteRenderer.enabled = false;

        controller.GetComponentInChildren<HurtBox>()?.Hurt(1.0f);
    }
}
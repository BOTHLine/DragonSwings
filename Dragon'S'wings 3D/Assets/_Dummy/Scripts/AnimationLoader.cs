using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationLoader : MonoBehaviour
{
    [HideInInspector] public Animator _Animator;
    [HideInInspector] public string[] _Animations;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();

        AnimatorControllerParameter[] parameters = _Animator.parameters;
        int animationCount = 0;
        foreach (AnimatorControllerParameter parameter in parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
                animationCount++;
        }
        _Animations = new string[animationCount];
        int animationIndex = 0;
        foreach (AnimatorControllerParameter parameter in parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
                _Animations[animationIndex++] = parameter.name;
        }
    }

    public void LoadAnimation(int animationIndex, Vector2 direction)
    {
        _Animator.SetTrigger(_Animations[animationIndex]);
        _Animator.SetFloat("X", direction.x);
        _Animator.SetFloat("Y", direction.y);
    }
}
using UnityEngine;

// TODO: Rename "ThrowResponder", "HookResponder", "HurtBoxResponder" etc.
public class ThrowResponder : MonoBehaviour
{
    public bool _AutoAim;
    public int _AutoAimPriority;

    public BoolReference _IsDamagableByThrow;
    public HurtBox _HurtBox;

    public void HitByThrow(HookResponder hookResponder)
    {

    }
}
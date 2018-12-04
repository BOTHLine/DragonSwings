using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Bool Variable")]
public class BoolVariable : BaseVariable<bool>
{
    public void Toggle()
    { Value = !Value; }
}
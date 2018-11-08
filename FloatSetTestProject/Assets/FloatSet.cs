using UnityEngine;

[CreateAssetMenu(menuName = "Sets/Float Set")]
public class FloatSet : BaseSet<float>
{
    public float ChangeValue(Transform identifier, float amount)
    {
        Items[identifier] += amount;
        return Items[identifier];
    }
}
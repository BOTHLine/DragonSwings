using UnityEngine;

[CreateAssetMenu(menuName = "Maps/Float Map")]
public class FloatMap : BaseMap<float>
{
    public float ChangeValue(GameObject identifier, float amount)
    {
        Items[identifier] += amount;
        return Items[identifier];
    }
}
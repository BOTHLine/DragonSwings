using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Maps/Float Map")]
public class FloatMap : BaseMap<float>
{
    public float ChangeValue(GameObject identifier, float amount)
    {
        if (!Items.ContainsKey(identifier))
        { Items.Add(identifier, 0.0f); }

        Items[identifier] += amount;
        return Items[identifier];
    }
}
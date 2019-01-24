using UnityEngine;
using System.Collections.Generic;

public enum ReferenceUseType
{
    Constant = 0,
    Variable = 1,
    Map = 2
}

[System.Serializable]
public class BaseReference<TVariable, TMap, TDatatype>
    where TVariable : BaseVariable<TDatatype>
    where TMap : BaseMap<TDatatype>
{

    public ReferenceUseType UseType = ReferenceUseType.Variable;

    public TDatatype ConstantValue;
    public TVariable Variable;
    public TMap Map;
    public GameObject MapIdentifier;

    private System.Action<TDatatype> OnValueChange = delegate { };
    private Dictionary<GameObject, System.Action<TDatatype>> OnValueChangeInMap = new Dictionary<GameObject, System.Action<TDatatype>>();

    public BaseReference() { }

    private static GameObject debugObject;

    public BaseReference(TDatatype value)
    {
        UseType = ReferenceUseType.Variable;
        ConstantValue = value;
    }

    public TDatatype Value
    {
        get
        {
            switch (UseType)
            {
                case ReferenceUseType.Constant:
                    return ConstantValue;
                case ReferenceUseType.Variable:
                    return Variable.Value;
                case ReferenceUseType.Map:
                    return Map.Get(MapIdentifier);

                default:
                    return ConstantValue;
            }
        }
        set
        {
            switch (UseType)
            {
                case ReferenceUseType.Constant:
                    ConstantValue = value;
                    OnValueChange.Invoke(Value);
                    break;
                case ReferenceUseType.Variable:
                    Variable.Value = value;
                    OnValueChange.Invoke(Value);
                    break;
                case ReferenceUseType.Map:
                    Map.Set(MapIdentifier, value);
                    System.Action<TDatatype> actions = GetActions(MapIdentifier);
                    actions.Invoke(Value);
                    // Debug.Log("Set static: " + MapIdentifier + ": " + OnValueChangeInMap[MapIdentifier].GetInvocationList().Length);
                    // Debug.Log("Compare Objects: " + debugObject + " and " + MapIdentifier + ": " + (debugObject == MapIdentifier));
                    break;
            }
        }
    }

    public TDatatype Get(GameObject mapIdentifier = null)
    {
        switch (UseType)
        {
            case ReferenceUseType.Constant:
                return ConstantValue;
            case ReferenceUseType.Variable:
                return Variable.Value;
            case ReferenceUseType.Map:
                return Map.Get(mapIdentifier == null ? MapIdentifier : mapIdentifier);
            default:
                return ConstantValue;
        }
    }

    public void Set(TDatatype value, GameObject mapIdentifier = null)
    {
        switch (UseType)
        {
            case ReferenceUseType.Constant:
                ConstantValue = value;
                OnValueChange.Invoke(Value);
                break;
            case ReferenceUseType.Variable:
                Variable.Value = value;
                OnValueChange.Invoke(Value);
                break;
            case ReferenceUseType.Map:
                Map.Set(mapIdentifier == null ? MapIdentifier : mapIdentifier, value);
                GetActions(mapIdentifier).Invoke(Value);
                // Debug.Log("Set dynamic: " + (mapIdentifier == null ? MapIdentifier : mapIdentifier) + ": " + OnValueChangeInMap[mapIdentifier == null ? MapIdentifier : mapIdentifier].GetInvocationList().Length);
                break;
        }
    }

    public void Subscribe(System.Action<TDatatype> action, GameObject mapIdentifier = null)
    {
        switch (UseType)
        {
            case ReferenceUseType.Constant:
                OnValueChange += action;
                break;
            case ReferenceUseType.Variable:
                OnValueChange += action;
                break;
            case ReferenceUseType.Map:
                System.Action<TDatatype> actions = GetActions(mapIdentifier);
                actions += action;
                OnValueChangeInMap[mapIdentifier == null ? MapIdentifier : mapIdentifier] = actions;
                // Debug.Log("Subscribe: " + (mapIdentifier == null ? MapIdentifier : mapIdentifier) + ": " + OnValueChangeInMap[mapIdentifier == null ? MapIdentifier : mapIdentifier].GetInvocationList().Length);
                // debugObject = mapIdentifier == null ? MapIdentifier : mapIdentifier;
                break;
        }
    }

    public void Unsubscribe(System.Action<TDatatype> action, GameObject mapIdentifier = null)
    {
        switch (UseType)
        {
            case ReferenceUseType.Constant:
                OnValueChange -= action;
                break;
            case ReferenceUseType.Variable:
                OnValueChange -= action;
                break;
            case ReferenceUseType.Map:
                System.Action<TDatatype> actions = GetActions(mapIdentifier);
                actions -= action;
                OnValueChangeInMap[mapIdentifier == null ? MapIdentifier : mapIdentifier] = actions;
                // Debug.Log("Unsubscribe " + (mapIdentifier == null ? MapIdentifier : mapIdentifier) + ": " + OnValueChangeInMap[mapIdentifier == null ? MapIdentifier : mapIdentifier].GetInvocationList().Length);
                break;
        }
    }

    private System.Action<TDatatype> GetActions(GameObject mapIdentifier)
    {
        System.Action<TDatatype> actions;
        OnValueChangeInMap.TryGetValue(mapIdentifier == null ? MapIdentifier : mapIdentifier, out actions);
        if (actions == null)
        {
            actions = delegate { };
            OnValueChangeInMap[mapIdentifier == null ? MapIdentifier : mapIdentifier] = actions;
        }
        return actions;
    }
}
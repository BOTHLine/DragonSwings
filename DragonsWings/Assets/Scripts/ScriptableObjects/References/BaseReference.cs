﻿using UnityEngine;
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

    public System.Action<TDatatype> OnValueChange = delegate { };
    public Dictionary<GameObject, System.Action<TDatatype>> OnValueChangeInMap;

    public BaseReference() { }

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
            OnValueChange.Invoke(Value);
            switch (UseType)
            {
                case ReferenceUseType.Constant:
                    ConstantValue = value;
                    break;
                case ReferenceUseType.Variable:
                    Variable.Value = value;
                    break;
                case ReferenceUseType.Map:
                    Map.Set(MapIdentifier, value);
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
                break;
            case ReferenceUseType.Variable:
                Variable.Value = value;
                break;
            case ReferenceUseType.Map:
                Map.Set(mapIdentifier == null ? MapIdentifier : mapIdentifier, value);
                break;
        }
        OnValueChange(Value);
    }

    public void Subscribe(System.Action<TDatatype> action, GameObject mapIdentifier = null)
    { OnValueChange += action; }

    public void Unsubscribe(System.Action<TDatatype> action, GameObject mapIdentifier = null)
    { OnValueChange -= action; }
}
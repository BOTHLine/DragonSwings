using UnityEngine;

public enum ReferenceUseType
{
    Constant = 0,
    Variable = 1,
    Map = 2
}

[System.Serializable]
public class BaseReference<TVariable, TSet, TDatatype>
    where TVariable : BaseVariable<TDatatype>
    where TSet : BaseMap<TDatatype>
{

    public ReferenceUseType UseType = ReferenceUseType.Variable;

    public TDatatype ConstantValue;
    public TVariable Variable;
    public TSet Map;
    public GameObject MapIdentifier;

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

    public static implicit operator TDatatype(BaseReference<TVariable, TSet, TDatatype> reference) { return reference.Value; }

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
    }
}
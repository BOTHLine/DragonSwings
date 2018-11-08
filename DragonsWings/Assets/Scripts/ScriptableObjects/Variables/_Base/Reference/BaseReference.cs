using UnityEngine;

public enum ReferenceUseType
{
    Constant = 0,
    Variable = 1,
    Set = 2
}

[System.Serializable]
public class BaseReference<TVariable, TSet, TDatatype>
    where TVariable : BaseVariable<TDatatype>
    where TSet : BaseMap<TDatatype>
{

    public ReferenceUseType UseType;

    public TDatatype ConstantValue;
    public TVariable Variable;
    public TSet Map;
    public Transform MapIdentifier;

    public BaseReference() { }

    public BaseReference(TDatatype value)
    {
        UseType = ReferenceUseType.Constant;
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
                case ReferenceUseType.Set:
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
                case ReferenceUseType.Set:
                    Map.Set(MapIdentifier, value);
                    break;
            }
        }
    }

    public static implicit operator TDatatype(BaseReference<TVariable, TSet, TDatatype> reference) { return reference.Value; }
}
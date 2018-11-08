using UnityEngine;

public enum UseType
{
    Constant = 0,
    Variable = 1,
    Set = 2
}

[System.Serializable]
public class BaseReference<TVariable, TSet, TDatatype>
    where TVariable : BaseVariable<TDatatype>
    where TSet : BaseSet<TDatatype>
{

    public UseType useType;

    public TDatatype constantValue;
    public TVariable variable;
    public TSet set;
    public Transform setIdentifier;

    public BaseReference() { }

    public BaseReference(TDatatype value)
    {
        useType = UseType.Constant;
        constantValue = value;
    }

    public TDatatype Value
    {
        get
        {
            switch (useType)
            {
                case UseType.Constant:
                    return constantValue;
                case UseType.Variable:
                    return variable.Value;
                case UseType.Set:
                    return set.Get(setIdentifier);

                default:
                    return constantValue;
            }
        }
        set
        {
            switch (useType)
            {
                case UseType.Constant:
                    constantValue = value;
                    break;
                case UseType.Variable:
                    variable.Value = value;
                    break;
                case UseType.Set:
                    set.Set(setIdentifier, value);
                    break;
            }
        }
    }

    public static implicit operator TDatatype(BaseReference<TVariable, TSet, TDatatype> reference) { return reference.Value; }
}
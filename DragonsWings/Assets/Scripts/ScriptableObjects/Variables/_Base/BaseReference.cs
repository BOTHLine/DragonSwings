using UnityEngine;

[System.Serializable]
public class BaseReference<TVariable, TDatatype>
    where TVariable : BaseVariable<TDatatype>
{
    public bool UseConstant = false;
    public TDatatype ConstantValue;
    public TVariable Variable;

    public BaseReference() { }

    public BaseReference(TDatatype value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public TDatatype Value { get { return UseConstant ? ConstantValue : Variable.Value; } set { if (UseConstant) ConstantValue = value; else Variable.Value = value; } }

    public static implicit operator TDatatype(BaseReference<TVariable, TDatatype> reference) { return reference.Value; }
}
using UnityEngine;

public abstract class BaseVariable<TDatatype> : ScriptableObject, ISerializationCallbackReceiver
{
    public TDatatype InitialValue;
    [SerializeField] protected TDatatype _Value;
    public TDatatype Value
    {
        get
        {
            return _Value;
        }
        set
        {
            _Value = value;
            OnValueChange.Invoke(_Value);
        }
    }

    public System.Action<TDatatype> OnValueChange = delegate { };

    public void SetValue(TDatatype value) { _Value = value; }
    public void SetValue(BaseVariable<TDatatype> value) { _Value = value.Value; }

    public void OnBeforeSerialize() { }
    public void OnAfterDeserialize() { _Value = InitialValue; }
}
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
            OnValueChange(_Value);
        }
    }

    private System.Action<TDatatype> OnValueChange = delegate { };

    public void SetValue(TDatatype value) { Value = value; }
    public void SetValue(BaseVariable<TDatatype> value) { Value = value.Value; }

    public void Subscribe(System.Action<TDatatype> action)
    { OnValueChange += action; }

    public void Unsubscribe(System.Action<TDatatype> action)
    { OnValueChange -= action; }

    public void OnBeforeSerialize() { }
    public void OnAfterDeserialize() { Value = InitialValue; }
}
public abstract class ActionMapBaseSet<TReference, TVariable, TMap, TDatatype> : Action
    where TReference : BaseReference<TVariable, TMap, TDatatype>
    where TVariable : BaseVariable<TDatatype>
    where TMap : BaseMap<TDatatype>
{
    public TMap _Map;
    public TReference _Value;

    public override void Act(StateController controller)
    { _Map.Set(controller.gameObject, _Value.Get(controller.gameObject)); }
}
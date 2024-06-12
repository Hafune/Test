namespace Core.Components
{
    [MyDoc("Ui значение")]
    public struct UiValue<T> : IUiValue where T : struct, IValue
    {
        public UiFloat data { get; set; }
    }
}
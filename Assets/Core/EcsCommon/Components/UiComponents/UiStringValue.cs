namespace Core.Components
{
    [MyDoc("Ui строковое значение")]
    public struct UiStringValue<T> : IUiStringValue where T : struct, IStringValue
    {
        public UiString data { get; set; }
    }
}
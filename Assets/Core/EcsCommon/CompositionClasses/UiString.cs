using System;

namespace Core.Components
{
    [Serializable]
    public class UiString
    {
        public static UiStringValue<T> BuildUiValueBy<T>(Func<string, string> formatter, Action<string> receiver) where T : struct, IStringValue
        {
            var component = default(UiStringValue<T>);
            component.data = new UiString(formatter, receiver);
            return component;
        }

        public static UiStringValue<T> BuildUiValueBy<T>(Action<string> refreshFunction) where T : struct, IStringValue
        {
            var component = default(UiStringValue<T>);
            component.data = new UiString(refreshFunction);
            return component;
        }
        
        private Action<string> _refreshFunction;

        private UiString(Action<string> refreshFunction) => _refreshFunction = refreshFunction;

        private UiString(Func<string, string> formatter, Action<string> receiver) =>
            _refreshFunction = v => receiver.Invoke(formatter.Invoke(v));

        public void RefreshUiView(string value) => _refreshFunction.Invoke(value);
    }
}
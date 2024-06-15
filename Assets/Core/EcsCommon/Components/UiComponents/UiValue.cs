using System;

namespace Core.Components
{
    [MyDoc("Ui значение")]
    public struct UiValue<T> where T : struct
    {
        public Action<T> update;
    }
}
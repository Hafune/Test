using System;

namespace Core.Components
{
    [Serializable]
    public class UiFloat
    {
        public event Action<float> RefreshFunction;

        public UiFloat(Action<float> refreshFunction) => RefreshFunction = refreshFunction;

        public UiFloat()
        {
        }

        public void RefreshUiView(float value) => RefreshFunction!.Invoke(value);
    }
}
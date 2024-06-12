using System;

namespace Core.Services
{
    public class BossHitPointBarService
    {
        public event Action OnShow;
        public event Action OnHide;
        public event Action OnChange;

        public float ValueMax { get; private set; }
        public float Value { get; private set; }

        public void Show() => OnShow?.Invoke();

        public void Hide() => OnHide?.Invoke();

        public void ChangeValue(float value)
        {
            Value = value;
            OnChange?.Invoke();
        }
        
        public void ChangeValueMax(float value)
        {
            ValueMax = value;
            OnChange?.Invoke();
        }
    }
}
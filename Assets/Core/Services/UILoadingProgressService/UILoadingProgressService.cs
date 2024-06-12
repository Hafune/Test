using System;

namespace Core.Services.UILoadingProgressService
{
    public class UILoadingProgressService
    {
        public event Action<float> OnPercentChange;
        
        public void SetPercent(float percent)
        {
            OnPercentChange?.Invoke(percent);
        }
    }
}
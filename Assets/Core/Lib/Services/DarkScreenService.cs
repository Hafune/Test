using System;

namespace Core
{
    public class DarkScreenService
    {
        public event Action OnFadeIn;
        public event Action OnFadeOut;
        
        private Action _callback;

        public void FadeIn(Action callback = null)
        {
            _callback = callback;
            OnFadeIn?.Invoke();
        }

        public void FadeOut(Action callback = null)
        {
            _callback = callback;
            OnFadeOut?.Invoke();
        }

        public void Complete() => _callback?.Invoke();
    }
}
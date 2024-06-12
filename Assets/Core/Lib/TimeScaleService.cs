using System;
using UnityEngine;

namespace Core.Lib
{
    public class TimeScaleService
    {
        public float TimeDilation { get; private set; } = 1;

        private int _currentScale;
        private float _lastOriginScale = 1;

        public void Pause()
        {
            CheckScale();
            _currentScale -= 1;
            Time.timeScale = Math.Clamp(_currentScale + TimeDilation, 0, 1);
            _lastOriginScale = Time.timeScale;
        }

        public void Resume()
        {
            CheckScale();
            _currentScale += 1;
            Time.timeScale = Math.Clamp(_currentScale + TimeDilation, 0, 1);
            _lastOriginScale = Time.timeScale;
        }

        public void SetTimeDilation(float timeScale)
        {
            CheckScale();
            TimeDilation = timeScale;
            Time.timeScale = Math.Clamp(_currentScale + TimeDilation, 0, 1);
            _lastOriginScale = Time.timeScale;
        }

        private void CheckScale()
        {
            if (_lastOriginScale != Time.timeScale)
                throw new Exception("Скейл был изменен из вне");
        }
    }
}
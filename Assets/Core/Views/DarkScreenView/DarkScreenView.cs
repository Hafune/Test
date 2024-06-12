using System;
using System.Collections;
using JetBrains.Annotations;
using Lib;
using UnityEngine;

namespace Core.Views.MainMenu
{
    public class DarkScreenView : AbstractUIDocumentView
    {
        private float _durationOut = 1f;
        private float _durationIn = .5f;
        private float _duration = 1.5f;
        private float _speed = 1f;
        private float _currentNormalizedTime;
        private int _direction;
        private DarkScreenService _service;
        [CanBeNull] private Coroutine _coroutine;

        protected override void Awake()
        {
            base.Awake();
            DisplayFlex();
            _service = Context.Resolve<DarkScreenService>();
            _service.OnFadeIn += () =>
            {
                _currentNormalizedTime = 0;
                _direction = 1;
                _duration = _durationIn;
                _coroutine ??= StartCoroutine(Progress());
            };
            _service.OnFadeOut += () =>
            {
                _currentNormalizedTime = 1;
                _direction = -1;
                _duration = _durationOut;
                _coroutine ??= StartCoroutine(Progress());
            };
        }

        private IEnumerator Progress()
        {
            while (true)
            {
                _currentNormalizedTime += _direction * _speed * Time.deltaTime / _duration;
                _currentNormalizedTime = Math.Clamp(_currentNormalizedTime, 0, 1);

                RootVisualElement.SetBackgroundColor(Color.Lerp(Color.clear, Color.black, _currentNormalizedTime));

                if (_currentNormalizedTime is 0 or 1)
                    break;

                yield return null;
            }

            _coroutine = null;
            _service.Complete();
        }
    }
}
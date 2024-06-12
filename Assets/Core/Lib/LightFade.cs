using System.Collections;
using UnityEngine;

namespace Core.Lib
{
    public class LightFade : MonoBehaviour
    {
        [SerializeField] private Light _light;
        [SerializeField] private float _targetRange;
        [SerializeField] private float _targetIntensity;
        [SerializeField] private float _time;
        private float _rangeStep;
        private float _intensityStep;
        private float _startRange;
        private float _startIntensity;
        private Coroutine _coroutine;

        private void OnValidate() => _light = _light ? _light : GetComponent<Light>();

        private void Awake()
        {
            _startRange = _light.range;
            _startIntensity = _light.intensity;
            _rangeStep = (_startRange - _targetRange) / _time;
            _intensityStep = (_startIntensity - _targetIntensity) / _time;
        }

        private void OnEnable()
        {
            _light.range = _startRange;
            _light.intensity = _startIntensity;
            _coroutine ??= StartCoroutine(Fade());
        }

        private void OnDisable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = null;
        }

        private IEnumerator Fade()
        {
            while (_targetIntensity != _light.intensity || _targetRange != _light.range)
            {
                _light.range = Mathf.MoveTowards(_light.range, _targetRange, _rangeStep * Time.deltaTime);
                _light.intensity =
                    Mathf.MoveTowards(_light.intensity, _targetIntensity, _intensityStep * Time.deltaTime);

                yield return null;
            }

            _coroutine = null;
        }
    }
}
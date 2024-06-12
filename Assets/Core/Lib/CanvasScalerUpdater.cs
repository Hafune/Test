using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Screen;

namespace Core.Lib
{
    [RequireComponent(typeof(CanvasScaler))]
    public class CanvasScalerUpdater : MonoBehaviour
    {
        [SerializeField] private CanvasScaler _сanvasScaler;

        private float _baseRatio;
        private float _lastCalc;

        private void OnValidate() => _сanvasScaler = _сanvasScaler ? _сanvasScaler : GetComponent<CanvasScaler>();

        private void Awake()
        {
            var baseAspectRation = _сanvasScaler.referenceResolution;
            _baseRatio = baseAspectRation.x / baseAspectRation.y;
        }

        private void FixedUpdate()
        {
            float w = width;
            float h = height;
            float ratio = w / h;
            float calc = Mathf.Max(0, Mathf.Min(1, ratio / _baseRatio));

            if (_lastCalc == calc)
                return;

            _lastCalc = calc;
            _сanvasScaler.matchWidthOrHeight = calc == 1 ? 1 : 0;
        }
    }
}
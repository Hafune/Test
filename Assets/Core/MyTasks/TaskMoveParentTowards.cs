using System;
using System.Collections;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskMoveParentTowards : MonoBehaviour, IMyTask
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _destinationPoint;
        [SerializeField] private float _time;
        [SerializeField] private AnimationCurve _curve;

        private Action<IMyTask> _onComplete;
        private Vector3 _childOffset;
        private Vector3 _startPosition;
        private Vector3 _finalPosition;
        private float _currentTime;

        private void OnValidate() => _destinationPoint = _destinationPoint ? _destinationPoint : transform;

        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _onComplete = onComplete;
            _startPosition = _target.position;
            _childOffset = _target.parent.position - _startPosition;
            _finalPosition = _destinationPoint.position;
            _currentTime = 0;
            InProgress = true;
            StartCoroutine(MyUpdate());
        }

        private IEnumerator MyUpdate()
        {
            while (true)
            {
                var pos = Vector3.Lerp(
                    _startPosition,
                    _finalPosition,
                    _curve.Evaluate((_currentTime += Time.deltaTime) / _time));

                _target.parent.position = pos + _childOffset;

                if (pos == _finalPosition)
                {
                    InProgress = false;
                    _onComplete?.Invoke(this);
                    yield break;
                }

                yield return null;
            }
        }
    }
}
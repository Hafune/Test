using System;
using System.Collections.Generic;
using System.Linq;
using Core.Lib;
using UnityEngine;
using VInspector;
using Random = UnityEngine.Random;

namespace Core.ExternalEntityLogics
{
    public class ProjectileEmitters2D : MonoBehaviour
    {
        [SerializeField] private SpreadPattern[] _patterns;
        [SerializeField] private bool _useRandomRange;

        [SerializeField, ShowIf(nameof(_useRandomRange))]
        private float _randomAngleRange;

        private IEnumerable<Transform> _emitters;
        private Vector3 _startEuler;

        private void Awake()
        {
            _startEuler = transform.localRotation.eulerAngles;
            _emitters = _patterns.SelectMany(i => i.EmittersList).Select(emitter => emitter.transform);
        }

        public void ForEachEmitter(Action<Transform> callback)
        {
            if (_useRandomRange)
                transform.localRotation =
                    Quaternion.Euler(_startEuler.x, _startEuler.y,
                        _startEuler.z - _randomAngleRange / 2 + Random.value * _randomAngleRange);

            foreach (var t in _emitters)
                callback.Invoke(t);
        }
    }
}
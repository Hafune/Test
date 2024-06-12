using System.Collections;
using System.Collections.Generic;
using Lib;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Components
{
    public class OnTriggerDispatcherOutsideWithRaycast : MonoBehaviour, ITriggerDispatcherTarget2D
    {
        [SerializeField] private MonoBehaviour _target;
        [SerializeField, Layer] private int _obstacleLayer;
        [SerializeField] private Transform _raycastStartPoint;
        private ITriggerDispatcherTarget2D _listener;
        private HashSet<Collider2D> _blockedContacts = new();
        private WaitForSeconds _wait = new(.2f);
        private Coroutine _coroutine;

        private void OnValidate()
        {
            _target = _target is ITriggerDispatcherTarget2D ? _target : null;
            _raycastStartPoint = _raycastStartPoint ? _raycastStartPoint : transform.root.Find("Bip001") ?? transform;

            if (_raycastStartPoint == null && _raycastStartPoint is not null)
                _raycastStartPoint = transform;
        }

        private void Awake()
        {
            Assert.IsNotNull(_raycastStartPoint);
            _listener = (ITriggerDispatcherTarget2D)_target;
        }

        private void OnDisable()
        {
            if (_coroutine is not null)
                StopCoroutine(_coroutine);

            _coroutine = null;
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (!TryAddTrigger(col))
                _blockedContacts.Add(col);

            if (_blockedContacts.Count > 0 && _coroutine is null)
                _coroutine = StartCoroutine(CheckBlockedContacts());
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            if (!_blockedContacts.Contains(col))
                _listener.OnTriggerExit2D(col);
            else
                _blockedContacts.Remove(col);
        }

        private IEnumerator CheckBlockedContacts()
        {
            while (_blockedContacts.Count != 0)
            {
                yield return _wait;

                _blockedContacts.RemoveWhere(TryAddTrigger);
            }

            _coroutine = null;
        }

        private bool TryAddTrigger(Collider2D col)
        {
            if (!col)
                return true;

            var pos = _raycastStartPoint.position;
            var dir = col.transform.position - pos;

            if (Physics2D.Raycast(pos, dir, dir.magnitude, 1 << _obstacleLayer))
                return false;

            _listener.OnTriggerEnter2D(col);
            return true;
        }
    }
}
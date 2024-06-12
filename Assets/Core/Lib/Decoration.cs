using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Core.Lib
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Decoration : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Material _hideMaterial;
        private Material _material;
        private bool _started;
        private bool _wasCalled;
        private object _watchCoroutine;
        private WaitForSeconds _wait = new(.5f);
        private bool _isInitialized;

        private void Start()
        {
            _material = _renderer.material;
            _isInitialized = true;
        }

        private void OnDisable()
        {
            _renderer.material.DOKill();
            Complete();
        }

        public void Hide()
        {
            _wasCalled = true;

            if (_watchCoroutine == null)
                _watchCoroutine = StartCoroutine(Watch());

            if (_started || !_isInitialized)
                return;

            _started = true;
            _renderer.material.DOKill();
            _renderer.material = _hideMaterial;
            _renderer.material.DOColor(new Color(1, 1, 1, .1f), .4f);
        }

        private void Complete()
        {
            _renderer.material = _material;
            _started = false;
        }

        private IEnumerator Watch()
        {
            while (_wasCalled)
            {
                _wasCalled = false;
                yield return _wait;
            }

            _renderer.material.DOKill();
            _renderer.material.DOColor(new Color(1, 1, 1, 1f), .4f).OnComplete(Complete);

            _watchCoroutine = null;
        }
    }
}
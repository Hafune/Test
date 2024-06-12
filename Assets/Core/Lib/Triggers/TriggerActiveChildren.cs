using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using VInspector;

namespace Core.Lib
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerActiveChildren : MonoBehaviour, ITrigger
    {
        [SerializeField, HideInInspector] private Collider2D _collider;
        [SerializeField] private float _executionDelay;
        [SerializeField] private bool _changeParent;

        [SerializeField,ShowIf("_changeParent")]
        private Transform _parent;

        private WaitForSeconds _delayWait;

        private void OnValidate() => _collider = GetComponent<Collider2D>();

        private void Awake()
        {
            Assert.IsNotNull(_collider);
            
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D _)
        {
            _collider.enabled = false;

            if (_changeParent)
                transform.parent = _parent;

            if (_executionDelay > 0)
            {
                _delayWait = new WaitForSeconds(_executionDelay);
                StartCoroutine(DelayedActivate());
                return;
            }

            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
        }

        private IEnumerator DelayedActivate()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                yield return _delayWait;
            }
        }
    }
}
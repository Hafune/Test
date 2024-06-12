using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Components
{
    public class ActiveWhileTriggered : MonoBehaviour, ITriggerDispatcherTarget2D
    {
        [SerializeField] private GameObject _target;

        private void Awake() => Assert.IsNotNull(_target);

        public void OnTriggerEnter2D(Collider2D col) => _target.SetActive(true);

        public void OnTriggerExit2D(Collider2D col) => _target.SetActive(false);
    }
}
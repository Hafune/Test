using UnityEngine;

namespace Core.Components
{
    public class OnTriggerDispatcherOutside : MonoBehaviour, ITriggerDispatcherTarget2D
    {
        [SerializeField] private MonoBehaviour _target;
        private ITriggerDispatcherTarget2D _listener;

        private void OnValidate() => _target = _target is ITriggerDispatcherTarget2D ? _target : null;

        private void Awake() => _listener = (ITriggerDispatcherTarget2D)_target;

        public void OnTriggerEnter2D(Collider2D col) => _listener.OnTriggerEnter2D(col);

        public void OnTriggerExit2D(Collider2D col) => _listener.OnTriggerExit2D(col);
    }
}
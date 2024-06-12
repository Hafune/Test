using UnityEngine;

namespace Core.Components
{
    public class OnTriggerDispatcher : MonoBehaviour, ITriggerDispatcherTarget2D
    {
        private ITriggerDispatcherTarget2D[] _listeners;
        private int _count;

        private void Awake()
        {
            _listeners = transform.parent.GetComponents<ITriggerDispatcherTarget2D>();
            _count = _listeners.Length;
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            for (int i = 0; i < _count; i++)
                _listeners[i].OnTriggerEnter2D(col);
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            for (int i = 0; i < _count; i++)
                _listeners[i].OnTriggerExit2D(col);
        }
    }
}
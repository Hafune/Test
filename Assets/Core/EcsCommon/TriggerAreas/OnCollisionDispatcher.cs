using UnityEngine;

namespace Core.Components
{
    public class OnCollisionDispatcher : MonoBehaviour
    {
        private ICollision2D[] _listeners;
        private int _count;

        private void Awake()
        {
            _listeners = transform.parent.GetComponents<ICollision2D>();
            _count = _listeners.Length;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            for (int i = 0; i < _count; i++)
                _listeners[i].OnCollisionEnter2D(col);
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            for (int i = 0; i < _count; i++)
                _listeners[i].OnCollisionExit2D(col);
        }
    }
}
using UnityEngine;

namespace Core
{
    public class PlatformCollisionController : MonoBehaviour
    {
        [SerializeField] private Collider2D _platformCollision;
        [SerializeField] private Collider2D _platformDispatcher;
        private int _contactCount;

        public void BeginIgnorePlatform()
        {
            if (_contactCount == 0)
                return;

            HeadTriggerCall();
        }

        public void HeadTriggerCall()
        {
            _platformCollision.gameObject.SetActive(false);
            _platformDispatcher.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col) => _contactCount++;

        private void OnTriggerExit2D(Collider2D col)
        {
            if (--_contactCount != 0)
                return;

            _platformCollision.gameObject.SetActive(true);
            _platformDispatcher.gameObject.SetActive(true);
        }
    }
}
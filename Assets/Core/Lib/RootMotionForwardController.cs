using UnityEngine;

namespace Core.Lib
{
    public class RootMotionForwardController : MonoBehaviour
    {
        [SerializeField] private AnimatorRootMotion _rootMotion;
        private int _contactCount;

        private void OnValidate()
        {
            if (_rootMotion)
                return;

            _rootMotion = GetComponentInParent<AnimatorRootMotion>();
        }

        private void OnEnable() => _contactCount = 0;

        private void OnDisable() => _rootMotion.EnableForwardMove();

        private void OnTriggerEnter2D(Collider2D col)
        {
            _contactCount++;

            if (!enabled || _contactCount != 1)
                return;

            _rootMotion.DisableForwardMove();
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            _contactCount--;

            if (!enabled || _contactCount != 0)
                return;

            _rootMotion.EnableForwardMove();
        }
    }
}
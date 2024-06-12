using UnityEngine;

namespace Core.Lib
{
    public class AnimatorOverridePosition : MonoBehaviour
    {
        [SerializeField] private bool _flipPositionX;

        private void LateUpdate()
        {
            if (!_flipPositionX)
                return;

            var pos = transform.localPosition;
            pos.x *= -1;
            transform.localPosition = pos;
        }
    }
}
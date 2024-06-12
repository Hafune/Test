using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class HideDecorationsByCameraFront : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _collider;

        private Collider[] _raycastHit = new Collider[128];

        private void FixedUpdate()
        {
            // var count = _collider.OverlapCapsuleNonAlloc(_raycastHit, 1 << Layers.Default.NameToLayer());
            var count = _collider.OverlapCapsuleNonAlloc(_raycastHit, 1 << Layers.Decoration.NameToLayer());

            if (count == 0)
                return;

            for (int i = 0; i < count; i++)
            {
                var hit = _raycastHit[i];

                var decoration = hit.transform.GetComponent<Decoration>();

                if (decoration == null)
                    continue;
                
                decoration.Hide();
            }
        }
    }
}
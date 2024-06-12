using UnityEngine;

namespace Core
{
    public class PlatformHeadTrigger : MonoBehaviour
    {
        [SerializeField] private PlatformCollisionController _platformCollisionController;

        private void OnTriggerEnter2D(Collider2D _) => _platformCollisionController.HeadTriggerCall();
    }
}
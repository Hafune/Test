using UnityEngine;

namespace Core.Lib
{
    public class TriggerTeleport : MonoBehaviour
    {
        [SerializeField] private Transform _destination;

        private void OnTriggerEnter2D(Collider2D col) => col.attachedRigidbody.position = _destination.position;
    }
}
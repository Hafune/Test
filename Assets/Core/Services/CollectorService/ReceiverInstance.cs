using UnityEngine;

namespace Core
{
    public class ReceiverInstance : MonoBehaviour
    {
        [SerializeField] private Collider _receiveArea;
        public void Activate()
        {
            _receiveArea.gameObject.SetActive(true);
        }
    }
}
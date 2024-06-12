using UnityEngine;

namespace Core.Lib
{
    public class DisableOnStart : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
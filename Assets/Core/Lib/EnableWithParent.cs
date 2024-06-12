using UnityEngine;

namespace Core.Lib
{
    public class EnableWithParent : MonoBehaviour
    {
        private EnableDispatcher _dispatcher;

        private void Awake()
        {
            _dispatcher = transform.parent.gameObject.AddComponent<EnableDispatcher>();
            _dispatcher.OnEnabled += () => gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            if (_dispatcher)
                Destroy(_dispatcher);
        }
    }
}
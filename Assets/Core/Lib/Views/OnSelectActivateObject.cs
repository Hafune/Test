using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core
{
    public class OnSelectActivateObject : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private GameObject _object;

        public void OnSelect(BaseEventData eventData) => _object.SetActive(true);

        public void OnDeselect(BaseEventData eventData) => _object.SetActive(false);
        
        private void OnDisable() => _object.SetActive(false);
    }
}
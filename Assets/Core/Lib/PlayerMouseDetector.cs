using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class PlayerMouseDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        public event Action<PointerEventData> PointerDown;
        public event Action<PointerEventData> PointerUp;
        public event Action<PointerEventData> PointerEnter;
        public event Action<PointerEventData> PointerExit;
        public event Action<PointerEventData> PointerMove;

        public void OnPointerDown(PointerEventData eventData) => PointerDown?.Invoke(eventData);
        
        public void OnPointerUp(PointerEventData eventData) => PointerUp?.Invoke(eventData);
        
        public void OnPointerEnter(PointerEventData eventData) => PointerEnter?.Invoke(eventData);
        
        public void OnPointerExit(PointerEventData eventData) => PointerExit?.Invoke(eventData);

        public void OnPointerMove(PointerEventData eventData) => PointerMove?.Invoke(eventData);
    }
}
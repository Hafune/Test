using UnityEngine;

namespace Core.BehaviorTree
{
    public class TriggerCounter2D : MonoBehaviour
    {
        public int Count { get; private set; }
        
        public bool TriggerIsActive() => Count > 0;

        private void OnEnable() => Count = 0;

        private void OnTriggerEnter2D(Collider2D col) => Count++;

        private void OnTriggerExit2D(Collider2D col) => Count--;
        
    }
}
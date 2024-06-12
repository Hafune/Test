using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Lib
{
    public class EffectTrigger : MonoBehaviour, ITriggerDispatcherTarget2D
    {
        [SerializeField] private AbstractEffect _effect;

#if UNITY_EDITOR
        private void Awake() => Assert.IsNotNull(_effect);
#endif

        public void OnTriggerEnter2D(Collider2D col) => _effect.Execute();

        public void OnTriggerExit2D(Collider2D col)
        {
        }
    }
}
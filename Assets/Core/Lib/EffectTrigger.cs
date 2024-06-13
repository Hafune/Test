using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Lib
{
    public class EffectTrigger : MonoBehaviour, ITriggerDispatcherTarget
    {
        [SerializeField] private AbstractEffect _effect;

#if UNITY_EDITOR
        private void Awake() => Assert.IsNotNull(_effect);
#endif

        public void OnTriggerEnter(Collider col) => _effect.Execute();

        public void OnTriggerExit(Collider col)
        {
        }
    }
}
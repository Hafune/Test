using UnityEngine;

namespace Core.Lib
{
    [RequireComponent(typeof(ParticleSystem))]
    public class DisableParentToo : MonoBehaviour
    {
        private GameObject _parent;

        private void Awake()
        {
            _parent = transform.parent.gameObject;
            var main = GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped() => _parent.SetActive(false);
    }
}
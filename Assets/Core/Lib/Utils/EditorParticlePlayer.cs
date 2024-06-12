using System.Collections;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
#endif

namespace Core.Lib
{
    [ExecuteInEditMode]
    public class EditorParticlePlayer : MonoBehaviour
    {
#if UNITY_EDITOR
        private ParticleSystem _particleSystem;
        private EditorCoroutine _coroutine;
        private float _timeScale = 1f;

        private void Awake() => hideFlags = HideFlags.DontSave;

        private void OnEnable()
        {
            _particleSystem = _particleSystem ? _particleSystem : GetComponent<ParticleSystem>();
            Play();
        }

        private void OnDisable()
        {
            if (_coroutine is not null)
                EditorCoroutineUtility.StopCoroutine(_coroutine);
        }

        public void Play()
        {
            if (!enabled)
                return;

            if (_coroutine is not null)
                EditorCoroutineUtility.StopCoroutine(_coroutine);

            _coroutine = EditorCoroutineUtility.StartCoroutineOwnerless(PlayPrivate());
        }

        public void SetTimeScale(float timeScale) => _timeScale = timeScale;

        private IEnumerator PlayPrivate()
        {
            var main = _particleSystem.main;
            float curTime = 0;

            double lastTimeSinceStartup = EditorApplication.timeSinceStartup;

            _particleSystem.Play(false);
            _particleSystem.Simulate(0, false, true);

            do
            {
                var deltaTime = (float)(EditorApplication.timeSinceStartup - lastTimeSinceStartup) * _timeScale;
                curTime += deltaTime;
                lastTimeSinceStartup = EditorApplication.timeSinceStartup;
                _particleSystem.Simulate(deltaTime, false, false);

                SceneView.RepaintAll();

                yield return null;
            } while (curTime < main.duration + 2 || _particleSystem.particleCount > 0);

            _particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

            if (_particleSystem.gameObject.hideFlags == HideFlags.DontSave)
                DestroyImmediate(_particleSystem.gameObject);

            _coroutine = null;
        }
#endif
    }
}
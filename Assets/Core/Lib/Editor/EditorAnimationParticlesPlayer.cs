using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core;
using Core.Lib;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lib
{
    [InitializeOnLoad]
    public class EditorAnimationParticlesPlayer
    {
        private static EditorCoroutine _coroutine;
        private static PrefabStage _prefabStage;
        private static List<GameObject> _spawnedObjects = new();
        private static readonly FieldInfo _getPrefabMethod;
        private static readonly FieldInfo _getSpawnInLocalMethod;
        private static readonly FieldInfo _getOffsetMethod;
        private static readonly FieldInfo _getQuaternionIdentityMethod;

        static EditorAnimationParticlesPlayer()
        {
            PrefabStage.prefabStageClosing += _ =>
            {
                Clear();
                EditorApplication.delayCall += OpenPrefabStageIfExist;
            };

            PrefabStage.prefabStageOpened += stage =>
            {
                Clear();
                ListenAnimationWindow(stage);
            };

            _getPrefabMethod = typeof(SpawnEffect).GetField("_prefab", BindingFlags.NonPublic | BindingFlags.Instance);
            _getOffsetMethod = typeof(SpawnEffect).GetField("_offset", BindingFlags.NonPublic | BindingFlags.Instance);
            _getQuaternionIdentityMethod =
                typeof(SpawnEffect).GetField("_quaternionIdentity", BindingFlags.NonPublic | BindingFlags.Instance);
            _getSpawnInLocalMethod =
                typeof(SpawnEffect).GetField("_attachEffect", BindingFlags.NonPublic | BindingFlags.Instance);

            OpenPrefabStageIfExist();
        }

        private static void Clear()
        {
            if (_coroutine is not null)
                EditorCoroutineUtility.StopCoroutine(_coroutine);

            _coroutine = null;

            foreach (var spawnedObject in _spawnedObjects)
                Object.DestroyImmediate(spawnedObject);

            _spawnedObjects.Clear();
            _prefabStage = null;
        }

        private static void OpenPrefabStageIfExist()
        {
            Clear();
            var stage = PrefabStageUtility.GetCurrentPrefabStage();
            if (!stage)
                return;

            ListenAnimationWindow(stage);
        }

        private static void ListenAnimationWindow(PrefabStage prefabStage)
        {
            if (Application.isPlaying)
                return;

            if (_coroutine is not null)
                EditorCoroutineUtility.StopCoroutine(_coroutine);

            _prefabStage = prefabStage;
            _coroutine = EditorCoroutineUtility.StartCoroutineOwnerless(ListenAnimationWindowCoroutine());
        }

        private static IEnumerator ListenAnimationWindowCoroutine()
        {
            AnimationWindow animationWindow = null;

            Reinitialize:

            while (!EditorWindow.HasOpenInstances<AnimationWindow>())
                yield return null;

            animationWindow = animationWindow != null
                ? animationWindow
                : EditorWindow.GetWindow<AnimationWindow>();

            while (!animationWindow.playing && EditorWindow.HasOpenInstances<AnimationWindow>())
                yield return null;

            var clip = animationWindow.animationClip;

            if (!clip)
            {
                yield return null;
                goto Reinitialize;
            }

            var events = clip?.events;

            if (events is null || events.Length == 0)
            {
                yield return null;
                goto Reinitialize;
            }

            var sentEvents = new HashSet<AnimationEvent>();
            float lastTime = 0;

            foreach (var o in _spawnedObjects)
            foreach (var player in o.GetComponentsInChildren<EditorParticlePlayer>())
                player.SetTimeScale(1);

            while (true)
            {
                if (!EditorWindow.HasOpenInstances<AnimationWindow>())
                    goto Reinitialize;

                if (clip != animationWindow.animationClip)
                    goto Reinitialize;

                if (!animationWindow.playing)
                {
                    foreach (var o in _spawnedObjects)
                    foreach (var player in o.GetComponentsInChildren<EditorParticlePlayer>())
                        player.SetTimeScale(0);

                    goto Reinitialize;
                }

                if (lastTime > animationWindow.time)
                {
                    lastTime = 0;
                    sentEvents.Clear();
                }

                float deltaTime = animationWindow.time - lastTime;
                lastTime = animationWindow.time;

                if (_prefabStage)
                    foreach (var animationEvent in events)
                        if (animationEvent.time + deltaTime <= animationWindow.time && sentEvents.Add(animationEvent))
                            SpawnEffect(_prefabStage.prefabContentsRoot, animationEvent, animationWindow);

                yield return null;
            }
        }

        private static void SpawnEffect(GameObject root, AnimationEvent animationEvent, AnimationWindow animationWindow)
        {
            if (!animationEvent.objectReferenceParameter ||
                animationEvent.objectReferenceParameter is not ReferencePath path)
                return;

            var obj = path.Find(root.transform);
            if (obj is null)
                return;

            var spawner = obj.GetComponent<SpawnEffect>();

            if (!spawner)
                return;

            var prefab = (Transform)_getPrefabMethod.GetValue(spawner);
            var hasParticles = (bool)prefab.GetComponent<ParticleSystem>();

            if (!hasParticles)
                return;

            bool inLocalSpace = (bool)_getSpawnInLocalMethod.GetValue(spawner);
            bool _quaternionIdentity = (bool)_getQuaternionIdentityMethod.GetValue(spawner);
            Vector3 offset = (Vector3)_getOffsetMethod.GetValue(spawner);

            var instance = Object.Instantiate(
                prefab.gameObject,
                spawner.transform.position,
                _quaternionIdentity ? Quaternion.identity : spawner.transform.rotation,
                root.transform
            );
            instance.transform.position = spawner.transform.position;
            instance.hideFlags = HideFlags.DontSave;

            foreach (var particleSystem in instance.GetComponentsInChildren<ParticleSystem>())
                particleSystem.gameObject.AddComponent<EditorParticlePlayer>();

            _spawnedObjects.Add(instance);
            EditorCoroutineUtility
                .StartCoroutineOwnerless(
                    SyncPosition(
                        instance,
                        spawner.transform,
                        spawner.transform.position,
                        inLocalSpace,
                        offset,
                        animationWindow));
        }

        private static IEnumerator SyncPosition(
            GameObject instance,
            Transform spawner,
            Vector3 startPosition,
            bool localSpace,
            Vector3 startOffset,
            AnimationWindow animationWindow
        )
        {
            var defaultLocalPosition = spawner.transform.localPosition;
            var startLocalPosition = spawner.localPosition;
            var worldSpaceParticles = instance.GetComponentsInChildren<ParticleSystem>()
                .Where(p => p.main.simulationSpace == ParticleSystemSimulationSpace.World).ToArray();

            while (instance &&
                   instance.activeSelf &&
                   animationWindow.rootVisualElement.visible &&
                   animationWindow.previewing)
            {
                var offset = spawner.transform.localPosition - defaultLocalPosition;
                instance.transform.position =
                    localSpace ? spawner.position + startOffset : startPosition + offset + startOffset;

                if (startLocalPosition != spawner.localPosition)
                {
                    var delta = spawner.localPosition - startLocalPosition;
                    startLocalPosition = spawner.localPosition;

                    for (int a = 0, iMax = worldSpaceParticles.Length; a < iMax; a++)
                    {
                        var particleSystem = worldSpaceParticles[a];
                        var count = particleSystem.particleCount;
                        var particles = new ParticleSystem.Particle[count];
                        particleSystem.GetParticles(particles);

                        for (int i = 0; i < particles.Length; i++)
                            particles[i].position += delta;

                        particleSystem.SetParticles(particles);
                    }
                }

                yield return null;
            }

            _spawnedObjects.Remove(instance);
            Object.DestroyImmediate(instance);
        }
    }
}
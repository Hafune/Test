#if UNITY_EDITOR
using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Lib
{
    public class EditorAnimationSetEvents : AbstractEditorBatchExecute
    {
        [SerializeField] private AnimationEventTemplate[] _events;
        [SerializeField] private string[] _animationNames;

        protected override IEnumerator ExecuteProtected(Transform[] suitable, Action next, Action cancel)
        {
            Selection.activeGameObject = suitable[0].GetComponent<Animator>().gameObject;
            yield return null;

            var names = _animationNames.Select(i => i.ToLower());
            var rawClips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            var clips = rawClips.Where(clip => names.Any(filterName => clip.name.ToLower().Contains(filterName)))
                .ToList();

            foreach (var clip in clips)
            foreach (var ev in _events)
            {
                var selfEvents = clip.events.ToList();

                if (selfEvents.Any(i => i.objectReferenceParameter == ev.obj))
                    continue;

                selfEvents.Add(new AnimationEvent
                {
                    time = clip.length * ev.normalizedTime,
                    objectReferenceParameter = ev.obj,
                    functionName = "PutAnimationEvent"
                });
                AnimationUtility.SetAnimationEvents(clip, selfEvents.ToArray());
                EditorUtility.SetDirty(clip);
            }

            yield return null;
            AssetDatabase.SaveAssets();

            yield return null;
            next();
        }

        [Serializable]
        private struct AnimationEventTemplate
        {
            public float normalizedTime;
            public Object obj;
        }
    }
}
#endif
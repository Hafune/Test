#if UNITY_EDITOR
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    public class EditorAnimationAttackSetEvents : AbstractEditorBatchExecute
    {
        [SerializeField] private AnimationEventTemplate[] _events;

        protected override IEnumerator ExecuteProtected(Transform[] suitable, Action next, Action cancel)
        {
            Selection.activeGameObject = suitable[0].GetComponent<Animator>().gameObject;
            yield return null;

            var rawClips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
            var names = rawClips
                .Where(clip => clip.name.ToLower().Contains("_end"))
                .Select(clip => clip.name.Replace("_end", "").Replace("_End", ""))
                .ToList();

            var clips = rawClips.Where(clip => names.Any(filterName => clip.name == filterName))
                .ToList();

            foreach (var clip in clips)
            foreach (var ev in _events)
            {
                var selfEvents = clip.events.ToList();

                if (selfEvents.Any(i => i.stringParameter == ev.stringParameter))
                    continue;

                selfEvents.Add(new AnimationEvent
                {
                    time = clip.length * ev.normalizedTime,
                    stringParameter = ev.stringParameter,
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
            public string stringParameter;
        }
    }
}
#endif
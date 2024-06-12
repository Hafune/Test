#if UNITY_EDITOR
using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class EditorAnimationEventsReplaceToSO : AbstractEditorBatchExecute
    {
        [SerializeField] private ActionAnimationEventData CanBeCanceled;
        [SerializeField] private ActionAnimationEventData CannotBeCanceled;
        [SerializeField] private ActionAnimationEventData Completed;
        [SerializeField] private ActionAnimationEventData ResetTargets;

        protected override IEnumerator ExecuteProtected(Transform[] suitable, Action next, Action cancel)
        {
            var rawClips = AnimationUtility.GetAnimationClips(suitable[0].GetComponent<Animator>().gameObject);

            foreach (var clip in rawClips)
            {
                var selfEvents = clip.events.ToList();
                bool dirty = false;

                foreach (var eEvent in selfEvents)
                {
                    if (string.IsNullOrEmpty(eEvent.stringParameter))
                        continue;

                    switch (eEvent.stringParameter)
                    {
                        case "CanBeCanceled":
                            eEvent.stringParameter = null;
                            eEvent.objectReferenceParameter = CanBeCanceled;
                            dirty = true;
                            break;
                        case "CannotBeCanceled":
                            eEvent.stringParameter = null;
                            eEvent.objectReferenceParameter = CannotBeCanceled;
                            dirty = true;
                            break;
                        case "Completed":
                            eEvent.stringParameter = null;
                            eEvent.objectReferenceParameter = Completed;
                            dirty = true;
                            break;
                        case "ResetTargets":
                            eEvent.stringParameter = null;
                            eEvent.objectReferenceParameter = ResetTargets;
                            dirty = true;
                            break;
                    }
                }

                if (!dirty)
                    continue;

                AnimationUtility.SetAnimationEvents(clip, selfEvents.ToArray());
                EditorUtility.SetDirty(clip);
            }

            yield return null;
            AssetDatabase.SaveAssets();

            yield return null;
            next();
        }
    }
}
#endif
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEditor;
using UnityEngine;
using VInspector;

namespace Core.Lib
{
    public class EditorAnimationWindowController : AbstractEditorBatchExecute
    {
        private Action _next;
        private Action _cancel;
        private AnimationWindow _animationWindow;
        private List<AnimationClip> _clips;

        protected override IEnumerator ExecuteProtected(Transform[] suitable, Action next, Action cancel)
        {
            _next = next;
            _cancel = cancel;
            Selection.activeGameObject = suitable[0].GetComponent<Animator>().gameObject;
            yield return null;

            _animationWindow = EditorWindow.GetWindow<AnimationWindow>();
            _animationWindow.Show();
            _clips = AnimationUtility.GetAnimationClips(Selection.activeGameObject).ToList();
        }

        [Button]
        [ButtonSize(22)]
        private void NextAnimation()
        {
            _animationWindow.animationClip = _clips.CircularNext(_animationWindow.animationClip);
        }

        [Button]
        [ButtonSize(22)]
        private void PreviousAnimation()
        {
            _animationWindow.animationClip = _clips.CircularPrevious(_animationWindow.animationClip);
        }

        [Button]
        [ButtonSize(22)]
        private void Next()
        {
            _animationWindow.Close();
            _next();
            _next = null;
            _cancel = null;
        }

        [Button]
        [ButtonSize(22)]
        private void Cancel()
        {
            _animationWindow.Close();
            _cancel();
            _next = null;
            _cancel = null;
        }
    }
}
#endif
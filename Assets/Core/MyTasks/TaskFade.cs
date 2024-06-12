using System;
using Reflex;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib
{
    public class TaskFade : MonoBehaviour, IMyTask
    {
        private enum Fade
        {
            In,
            Out
        }

        [SerializeField] private Fade _fade = Fade.In;
        private Action<IMyTask> _onComplete;
        public bool InProgress => false;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;
            
            name = $"Fade_{Enum.GetName(typeof(Fade), _fade)}";
            EditorUtility.SetDirty(this);
        }
#endif
        
        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _onComplete = onComplete;

            switch (_fade)
            {
                case Fade.In:
                    context.Resolve<DarkScreenService>().FadeIn(OnComplete);
                    break;
                case Fade.Out:
                    context.Resolve<DarkScreenService>().FadeOut(OnComplete);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnComplete() => _onComplete?.Invoke(this);
    }
}
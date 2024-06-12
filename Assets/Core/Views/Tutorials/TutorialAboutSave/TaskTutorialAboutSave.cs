using System;
using System.Collections;
using Core.Views.MainMenu;
using LurkingNinja.MyGame.Internationalization;
using Reflex;
using UnityEngine;

namespace Core.Views
{
    public class TaskTutorialAboutSave : MonoBehaviour, IMyTask
    {
        [SerializeField] private Transform _checkPoint;
        private TutorialFocusView _focus;
        private Action<IMyTask> _onComplete;

        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _onComplete = onComplete;
            _focus = context.Resolve<TutorialFocusView>();
            context.Resolve<Camera>();
            InProgress = true;
            Run(OnComplete);
        }

        private void OnComplete()
        {
            InProgress = false;
            _onComplete?.Invoke(this);
        }

        private void Run(Action callback)
        {
            _focus.Show();
            StartCoroutine(RunScript(callback));
        }

        private IEnumerator RunScript(Action callback)
        {
            using var w = new TutorialWaiting();
            Rect rect = new(0, 0, 200, 300);
            rect.center = _focus.TransformWorldToPanel(_checkPoint.position);

            yield return w.WaitFrames();
            _focus.Select(rect);
            _focus.ChangeText(I18N.TutorialAboutSave.about_save);

            yield return w.WaitSubmit();
            _focus.Select(new Rect(0, 0, 800, 200));
            _focus.ChangeText(I18N.TutorialAboutSave.about_restore);

            yield return w.WaitSubmit();

            _focus.Hide();
            callback.Invoke();
        }
    }
}
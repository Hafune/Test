using UnityEngine.UIElements;

namespace Core
{
    public class LoopListManipulator : IManipulator
    {
        private ScrollView _target;

        public VisualElement target
        {
            get => _target;
            set
            {
                if (target is not null)
                    UnregisterCallback();

                _target = (ScrollView)value;
                RegisterCallbacks();
            }
        }

        private void RegisterCallbacks() => _target.RegisterCallback<NavigationMoveEvent>(Action);

        private void UnregisterCallback() => _target.UnregisterCallback<NavigationMoveEvent>(Action);

        private void Action(NavigationMoveEvent e)
        {
            if (_target.contentContainer.childCount <= 1)
                return;

            var focus = _target.focusController.focusedElement;
            var firstEle = _target.contentContainer.ElementAt(0);
            var lastEle =
                _target.contentContainer.ElementAt(
                    _target.contentContainer.childCount - 1);

            switch (e.direction)
            {
                case NavigationMoveEvent.Direction.Up when focus == firstEle:
                    e.PreventDefault();
                    lastEle.Focus();
                    break;
                case NavigationMoveEvent.Direction.Down when focus == lastEle:
                    e.PreventDefault();
                    firstEle.Focus();
                    break;
            }
        }
    }
}
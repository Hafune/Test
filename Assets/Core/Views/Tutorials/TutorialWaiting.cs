using System;
using System.Collections;

namespace Core.Views.MainMenu
{
    public class TutorialWaiting : IDisposable
    {
        private bool _rightTrigger;
        private bool _submit;
        private bool _cancel;
        private bool _inventory;
        private PlayerInputs.UIActions _uiActions;

        public TutorialWaiting()
        {
            _uiActions = new PlayerInputs().UI;
            _uiActions.Inventory.performed += _ => _inventory = true;
            _uiActions.Submit.performed += _ => _submit = true;
            _uiActions.Cancel.performed += _ => _cancel = true;
            _uiActions.RightTrigger.performed += _ => _rightTrigger = true;
            _uiActions.Enable();
        }

        public IEnumerator WaitSubmit()
        {
            _submit = false;
            
            while (!_submit)
                yield return null;
            _submit = false;

            yield return WaitFrames();
        }

        public IEnumerator WaitCancel()
        {
            _cancel = false;
            
            while (!_cancel)
                yield return null;
            _cancel = false;

            yield return WaitFrames();
        }

        public IEnumerator WaitInventory()
        {
            _inventory = false;
            
            while (!_inventory)
                yield return null;
            _inventory = false;

            yield return WaitFrames();
        }

        public IEnumerator WaitRightTrigger()
        {
            _rightTrigger = false;
            
            while (!_rightTrigger)
                yield return null;
            _rightTrigger = false;

            yield return WaitFrames();
        }

        public IEnumerator WaitFrames()
        {
            yield return null;
            yield return null;
            yield return null;
            yield return null;
        }

        public void Dispose() => _uiActions.Disable();
    }
}
using Core.Services.SpriteForInputService;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.InputSprites
{
    public abstract class AbstractInputSpriteElement : VisualElement, IContextElement
    {
        private InputSpritesService _service;
        private readonly InputSpritesService.InputKeys _inputKey;

        protected AbstractInputSpriteElement(InputSpritesService.InputKeys inputKey)
        {
            _inputKey = inputKey;

#if UNITY_EDITOR
            if (Application.isPlaying)
                return;

            var spritesData =
                (EditorDefaultInputSpritesData)Resources.Load("InputSprites/" + nameof(EditorDefaultInputSpritesData));
            this.SetBackgroundImage(spritesData.Data.GetSprite(_inputKey));
#endif
        }

        public void SetupContext(Context context)
        {
            _service = context.Resolve<InputSpritesService>();
            _service.OnChange += ReloadData;
            ReloadData();
        }

        private void ReloadData() => this.SetBackgroundImage(_service.GetSprite(_inputKey));
    }
}
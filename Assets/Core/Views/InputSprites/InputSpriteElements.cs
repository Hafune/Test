using Core.Services.SpriteForInputService;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Core.InputSprites
{
    public class JumpInputSpriteElement : AbstractInputSpriteElement
    {
        public JumpInputSpriteElement() : base(InputSpritesService.InputKeys.Jump)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<JumpInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class DashInputSpriteElement : AbstractInputSpriteElement
    {
        public DashInputSpriteElement() : base(InputSpritesService.InputKeys.Dash)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<DashInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
    
    public class LightAttackInputSpriteElement : AbstractInputSpriteElement
    {
        public LightAttackInputSpriteElement() : base(InputSpritesService.InputKeys.LightAttack)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<LightAttackInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class StrongAttackInputSpriteElement : AbstractInputSpriteElement
    {
        public StrongAttackInputSpriteElement() : base(InputSpritesService.InputKeys.StrongAttack)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<StrongAttackInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class SubmitInputSpriteElement : AbstractInputSpriteElement
    {
        public SubmitInputSpriteElement() : base(InputSpritesService.InputKeys.Submit)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<SubmitInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class CancelInputSpriteElement : AbstractInputSpriteElement
    {
        public CancelInputSpriteElement() : base(InputSpritesService.InputKeys.Cancel)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<CancelInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class ContextMenuInputSpriteElement : AbstractInputSpriteElement
    {
        public ContextMenuInputSpriteElement() : base(InputSpritesService.InputKeys.ContextMenu)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<ContextMenuInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class UpInputSpriteElement : AbstractInputSpriteElement
    {
        public UpInputSpriteElement() : base(InputSpritesService.InputKeys.Up)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<UpInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class DownInputSpriteElement : AbstractInputSpriteElement
    {
        public DownInputSpriteElement() : base(InputSpritesService.InputKeys.Down)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<DownInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class RightInputSpriteElement : AbstractInputSpriteElement
    {
        public RightInputSpriteElement() : base(InputSpritesService.InputKeys.Right)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<RightInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class LeftInputSpriteElement : AbstractInputSpriteElement
    {
        public LeftInputSpriteElement() : base(InputSpritesService.InputKeys.Left)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<LeftInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class StartInputSpriteElement : AbstractInputSpriteElement
    {
        public StartInputSpriteElement() : base(InputSpritesService.InputKeys.Start)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<StartInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class SelectInputSpriteElement : AbstractInputSpriteElement
    {
        public SelectInputSpriteElement() : base(InputSpritesService.InputKeys.Select)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<SelectInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class RightTriggerInputSpriteElement : AbstractInputSpriteElement
    {
        public RightTriggerInputSpriteElement() : base(InputSpritesService.InputKeys.RightTrigger)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<RightTriggerInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }

    public class LeftTriggerInputSpriteElement : AbstractInputSpriteElement
    {
        public LeftTriggerInputSpriteElement() : base(InputSpritesService.InputKeys.LeftTrigger)
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<LeftTriggerInputSpriteElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}
using Core.InputSprites;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Views.MainMenu
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class AbstractUIDocumentView : MonoConstruct
    {
        [SerializeField] private UIDocument _document;
        protected Context Context { get; private set; }
        public VisualElement RootVisualElement => _document.rootVisualElement;

        private void OnValidate() => _document = _document ? _document : GetComponent<UIDocument>();

        protected override void Construct(Context context) => Context = context;

        protected virtual void Awake()
        {
            _document.rootVisualElement.DisplayNone();
            SetPickingModeIgnore(_document.rootVisualElement);
            RootVisualElement.Query<VisualElement>()
                .Where(ele => ele is IContextElement)
                .ForEach(ele => ((IContextElement)ele).SetupContext(Context));
        }

        protected void DisplayFlex() => _document.rootVisualElement.DisplayFlex();

        protected void DisplayNone() => _document.rootVisualElement.DisplayNone();
        
        /// <summary>
        /// Устанавливает <b>PickingMode.Ignore</b> для всех элементов дерева 
        /// </summary>
        private static void SetPickingModeIgnore(VisualElement element)
        {
            element.pickingMode = PickingMode.Ignore;
            SetPickingModeIgnoreRecursive(element);
        }

        private static void SetPickingModeIgnoreRecursive(VisualElement element)
        {
            if (element.focusable)
                return;

            element.pickingMode = PickingMode.Ignore;

            foreach (var child in element.hierarchy.Children())
                SetPickingModeIgnoreRecursive(child);
        }
    }
}
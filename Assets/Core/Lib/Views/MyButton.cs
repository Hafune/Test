using Lib;
using Reflex;
using UnityEngine.UI;

namespace Core.Views
{
    public abstract class MyButton : MonoConstruct
    {
        private Button _button;

        protected Context Context { get; private set; }

        protected override void Construct(Context context) => Context = context;

        protected virtual void Awake()
        {
            _button = GetComponentInChildren<Button>();
            _button.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}
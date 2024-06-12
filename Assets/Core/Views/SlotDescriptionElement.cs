using System.Collections.Generic;
using Core.Components;
using Core.EcsCommon.ValueComponents;
using Core.Generated;
using Core.InputSprites;
using Core.Systems;
using JetBrains.Annotations;
using Lib;
using LurkingNinja.MyGame.Internationalization;
using Reflex;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Core
{
    public class SlotDescriptionElement : VisualElement, IContextElement
    {
        private SlotDescriptionVT _root;
        private VisualElement _rowPrefab;
        private VisualElement _tagPrefab;
        private readonly List<SlotDescriptionVT> _rowElePool = new();
        private readonly List<SlotDescriptionVT> _tagElePool = new();
        private readonly List<VisualElement> _rowPool = new();
        private readonly List<VisualElement> _tagPool = new();
        private ValueEnumIcons _valueEnumIcons;

        public void SetupContext(Context context) => _valueEnumIcons = context.Resolve<ValueEnumIcons>();

        public void Refresh(IReadOnlyList<ValueData> values, [CanBeNull] IReadOnlyList<SlotTagEnum> tags)
        {
            if (_root is null)
            {
                _root = new SlotDescriptionVT(this);
                _rowPrefab ??= _root.row;
                _tagPrefab ??= _root.tagText;
            }

            _root.container.Clear();
            _root.tagContainer.Clear();

            while (_rowElePool.Count < values.Count)
            {
                var ele = _rowPrefab.SimpleCopyHierarchy();
                _rowPool.Add(ele);
                _rowElePool.Add(new SlotDescriptionVT(ele));
            }

            for (int i = 0, iMax = values.Count; i < iMax; i++)
            {
                var valueData = values[i];
                var key = MyEnumUtility<ValueEnum>.Name((int)valueData.valueEnum);
                var value = valueData.value;
                var row = _rowPool[i];
                var rowElems = _rowElePool[i];

                if (!I18N.ValueEnumNames.Table.HasLocalizedString(key))
                    continue;

                rowElems.valueIcon.SetBackgroundImage(_valueEnumIcons.GetSprite(valueData.valueEnum));
                rowElems.value.text = ValueUtility.Format(valueData.valueEnum, value);
                rowElems.valueName.text = I18N.ValueEnumNames.Table.GetLocalizedString(key);

                _root.container.Add(row);
            }

            return;
            if (tags is null)
                return;

            while (_tagElePool.Count < tags.Count)
            {
                var ele = _tagPrefab.SimpleCopyHierarchy();
                _tagPool.Add(ele);
                _tagElePool.Add(new SlotDescriptionVT(ele));
            }

            for (int i = 0, iMax = tags.Count; i < iMax; i++)
            {
                var valueEnum = tags[i];
                var tag = _tagPool[i];
                var tagElems = _tagElePool[i];
                tagElems.tagText.text =
                    I18N.SlotTagNames.Table.GetLocalizedString(MyEnumUtility<SlotTagEnum>.Name((int)valueEnum));
                _root.tagContainer.Add(tag);
            }
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<SlotDescriptionElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}
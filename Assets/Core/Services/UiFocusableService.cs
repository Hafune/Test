using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Core
{
    public class UiFocusableService
    {
        private readonly Stack<FocusableNodeElement> _focusableLayers = new();
        private readonly Stack<List<VisualElement>> _pickingLayers = new();
        [ItemCanBeNull] private readonly Stack<Focusable> _focusableItems = new();

        public void AddLayer(VisualElement ele)
        {
            if (_focusableLayers.Count > 0)
            {
                var node = _focusableLayers.Peek();
                _focusableItems.Push(node.focusController.focusedElement);
                node.SetActive(false);

                foreach (var nodeElement in _pickingLayers.Peek())
                    nodeElement.pickingMode = PickingMode.Ignore;
            }

            var n = FindFocusableNode(ele);
            _focusableLayers.Push(n);
            n.SetActive(true);

            List<VisualElement> pickingList = new();
            CollectPickingElements(ele, pickingList);
            _pickingLayers.Push(pickingList);
        }

        public void RemoveLayer()
        {
            _focusableLayers.Pop();
            _pickingLayers.Pop();

            if (_focusableLayers.Count > 0)
            {
                var node = _focusableLayers.Peek();
                node.SetActive(true);

                foreach (var nodeElement in _pickingLayers.Peek())
                    nodeElement.pickingMode = PickingMode.Position;
            }

            if (_focusableItems.Count <= 0)
                return;

            _focusableItems.Pop()?.Focus();
        }

        private FocusableNodeElement FindFocusableNode(VisualElement ele)
        {
            if (ele is FocusableNodeElement node)
                return node;

            return ele
                .Children()
                .Select(FindFocusableNode)
                .FirstOrDefault(n => n != null);
        }

        private void CollectPickingElements(VisualElement ele, List<VisualElement> list)
        {
            if (ele is IFocusableElement or FocusableNodeElement)
                return;

            if (ele.pickingMode == PickingMode.Position)
                list.Add(ele);

            foreach (var child in ele.Children())
                CollectPickingElements(child, list);
        }
    }
}
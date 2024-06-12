using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIDocumentLocalization : MonoBehaviour
{
    private readonly Dictionary<VisualElement, string> _originalTexts = new();

    [SerializeField] private LocalizedStringTable _table;
    private UIDocument _uiDocument;
    private VisualElement _rootEle;

    private void OnEnable()
    {
        if (_uiDocument == null)
            _uiDocument = GetComponent<UIDocument>();

        _rootEle = _uiDocument.rootVisualElement;
        
        _table.TableChanged += OnTableChanged;

        Assert.IsNotNull(_rootEle);
    }

    private void OnDisable() => _table.TableChanged -= OnTableChanged;


    private void OnTableChanged(StringTable table)
    {
        var op = _table.GetTableAsync();
        
        if (op.IsDone)
        {
            OnTableLoaded(op);
        }
        else
        {
            op.Completed -= OnTableLoaded;
            op.Completed += OnTableLoaded;
        }
    }

    private void OnTableLoaded(AsyncOperationHandle<StringTable> op)
    {
        var table = op.Result;
        LocalizeChildrenRecursively(_rootEle, table);
        _rootEle.MarkDirtyRepaint();
    }

    private void LocalizeChildrenRecursively(VisualElement element, StringTable table)
    {
        var elementHierarchy = element.hierarchy;
        int numChildren = elementHierarchy.childCount;

        for (int i = 0; i < numChildren; i++)
        {
            var child = elementHierarchy.ElementAt(i);
            Localize(child, table);
        }

        for (int i = 0; i < numChildren; i++)
        {
            var child = elementHierarchy.ElementAt(i);
            var childHierarchy = child.hierarchy;
            int numGrandChildren = childHierarchy.childCount;
            if (numGrandChildren != 0)
                LocalizeChildrenRecursively(child, table);
        }
    }

    private void Localize(VisualElement next, StringTable table)
    {
        if (_originalTexts.TryGetValue(next, out var text))
            ((TextElement)next).text = text;

        if (next is not TextElement textElement)
            return;

        string key = textElement.text;

        if (string.IsNullOrEmpty(key) || key[0] != '#')
            return;

        if (!_originalTexts.ContainsKey(textElement))
            _originalTexts.Add(textElement, textElement.text);

        key = key.TrimStart('#');
        var entry = table[key];

        if (entry != null)
            textElement.text = entry.LocalizedValue;
    }
}
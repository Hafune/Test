using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

public class UIElementLocalization
{
    private readonly Dictionary<VisualElement, string> _originalTexts = new();

    private LocalizedStringTable _table;
    private StringTable _ltable;
    private VisualElement _rootEle;

    public UIElementLocalization(VisualElement rootEle, string tableName)
    {
        _rootEle = rootEle;
        var asyncHandle = LocalizationSettings.StringDatabase.GetTableAsync(tableName);
        asyncHandle.Completed += WaitTableLoading;
    }

    public UIElementLocalization(VisualElement rootEle, LocalizedStringTable table)
    {
        _table = table;
        _rootEle = rootEle;
        Enable();
    }

    private void WaitTableLoading(AsyncOperationHandle<StringTable> handle)
    {
        var stringTable = handle.Result;
        _table = new LocalizedStringTable { TableReference = stringTable.TableCollectionName };
        Enable();
    }

    public void Enable()
    {
        if (_table is not null)
            _table.TableChanged += OnTableChanged;
    }

    public void Disable()
    {
        if (_table is not null)
            _table.TableChanged -= OnTableChanged;
    }

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
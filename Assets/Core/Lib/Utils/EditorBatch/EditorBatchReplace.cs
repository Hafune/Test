#if UNITY_EDITOR
using System;
using System.Collections;
using Core.Lib;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

public class EditorBatchReplace : AbstractEditorBatchExecute
{
    [SerializeField] public GameObject prefab;
    [SerializeField] private bool _quaternionIdentity;
    [SerializeField] private bool _saveOldName;

    private void OnValidate() => Rename();

    public void Rename()
    {
        if (prefab)
            gameObject.name = "Replace " + prefab.name;
    }

    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        foreach (var i in suitable)
        {
            yield return null;

            if (_quaternionIdentity)
                i.rotation = Quaternion.identity;

            var replacementName = i.name;
            var obj = EditorReplacePrefab.Replace(i, prefab, false);

            if (!_saveOldName)
                continue;

            obj.name = replacementName;
            yield return null;
        }

        yield return null;

        callback?.Invoke();
    }
}

#endif
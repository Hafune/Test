#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class EditorBatchRename : AbstractEditorBatchExecute
{
    [SerializeField] private string _newName;

    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        foreach (var i in suitable)
        {
            yield return null;

            i.name = _newName;
            EditorUtility.SetDirty(i);
        }

        yield return null;
        
        callback?.Invoke();
    }
}
#endif
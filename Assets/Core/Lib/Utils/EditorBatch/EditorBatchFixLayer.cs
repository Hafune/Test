#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using UnityEditor;

public class EditorBatchFixLayer : AbstractEditorBatchExecute
{
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel = null)
    {
        foreach (var i in suitable)
        {
            i.gameObject.layer = 0;
            EditorUtility.SetDirty(i.gameObject);
        }

        yield return null;

        callback?.Invoke();
    }
}

#endif
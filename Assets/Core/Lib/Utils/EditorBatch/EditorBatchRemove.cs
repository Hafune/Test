#if UNITY_EDITOR
using System;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EditorBatchRemove : AbstractEditorBatchExecute
{
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        foreach (var i in suitable)
        {
            yield return null;

            if (i.IsDestroyed()) 
                continue;
            
            EditorUtility.SetDirty(i.parent.gameObject);
            DestroyImmediate(i.gameObject);
        }

        yield return null;

        callback?.Invoke();
    }
}
#endif

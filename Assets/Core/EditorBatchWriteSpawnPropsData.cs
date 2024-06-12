#if UNITY_EDITOR
using System;
using System.Collections;
using Core.Lib;
using UnityEditor;
using UnityEngine;

public class EditorBatchWriteSpawnPropsData : AbstractEditorBatchExecute
{
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        foreach (var i in suitable)
        {
            foreach (var data in i.GetComponentsInChildren<SpawnPropsData>())
                data.SerializeProps();

            EditorUtility.SetDirty(i);
        }

        yield return null;

        callback?.Invoke();
    }
}
#endif
#if UNITY_EDITOR
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EditorBatchRemoveComponent : AbstractEditorBatchExecute
{
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        foreach (var i in suitable)
        {
            if (i.TryGetComponent<MeshRenderer>(out var meshRenderer))
                DestroyImmediate(meshRenderer);

            if (i.TryGetComponent<MeshFilter>(out var meshFilter))
                DestroyImmediate(meshFilter);

            if (i.TryGetComponent<Collider>(out var col))
                DestroyImmediate(col);

            EditorUtility.SetDirty(i);
        }

        yield return null;

        callback?.Invoke();
    }
}
#endif
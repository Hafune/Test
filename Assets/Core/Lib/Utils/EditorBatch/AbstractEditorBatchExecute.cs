#if UNITY_EDITOR
using System;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

public abstract class AbstractEditorBatchExecute : MonoBehaviour
{
    public void Execute(Transform[] children, Action next, Action cancel) =>
        EditorCoroutineUtility.StartCoroutine(ExecuteProtected(children, next, cancel), gameObject);

    protected abstract IEnumerator ExecuteProtected(Transform[] suitable, Action next, Action cancel);
}
#endif
#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class EditorBatchAnimatorWriteDefaultValuesOnDisable : AbstractEditorBatchExecute
{
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        foreach (var i in suitable)
        {
            var animator = i.GetComponent<Animator>(); 
            animator.writeDefaultValuesOnDisable = true;
            EditorUtility.SetDirty(animator);
        }

        yield return null;

        callback?.Invoke();
    }
}
#endif
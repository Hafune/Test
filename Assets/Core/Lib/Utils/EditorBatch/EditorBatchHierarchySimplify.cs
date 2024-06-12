#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    public class EditorBatchHierarchySimplify : AbstractEditorBatchExecute
    {
        protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
        {
            foreach (var child in suitable)
            {
                while (child.childCount > 0)
                    child.GetChild(0).SetParent(child.transform.parent);
                
                EditorUtility.SetDirty(child);
            }

            yield return null;

            callback?.Invoke();
        }
    }
}
#endif
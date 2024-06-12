#if UNITY_EDITOR
using System;
using System.Collections;
using Core.Systems;
using Lib;
using UnityEditor;
using UnityEngine;

public class EditorBatchCustomForEach : AbstractEditorBatchExecute
{
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        foreach (var i in suitable)
        {
            // var olds = i.GetComponentsInChildren<ActionEntityCompositeLogicOld>(true);
            //
            // foreach (var oldAction in olds)
            // {
            //     if (!oldAction.gameObject.TryGetComponent<ActionEntityCompositeLogic>(out var action))
            //         action = oldAction.gameObject.AddComponent<ActionEntityCompositeLogic>();
            //
            //     action._condition = oldAction._conditionLogic;
            //     action._start = ExtractRootLogic(oldAction._startLogicContainer);
            //     action._update = ExtractRootLogic(oldAction._updateLogicContainer);
            //     action._cancel = ExtractRootLogic(oldAction._cancelLogicContainer);
            //     action._completeStreaming = ExtractRootLogic(oldAction._completeStreamingLogicContainer);
            //     action._resetOnStart = oldAction._resetOnStart;
            //
            //     if (oldAction._timelineActionLogicContainer)
            //         Debug.LogWarning("Timeline! " + i.name + oldAction.gameObject.GetPath(), oldAction.gameObject);
            // }

            // foreach (var oldAction in olds)
            // {
            //     DestroyImmediate(oldAction);
            //     EditorUtility.SetDirty(i);
            // }
            // if (i.GetComponentInChildren<MapBlockExitMarker>(true) == null)
            //     Debug.LogWarning(i.name);
        }

        callback?.Invoke();
        yield break;
    }

    private AbstractEntityLogic ExtractRootLogic(GameObject go)
    {
        if (!go)
            return null;

        if (go.TryGetComponent<EntityLogicNode>(out var node))
        {
            if (go.GetComponents<AbstractEntityLogic>().Length > 1)
                throw new Exception("есть и нод и экшены");

            return node;
        }

        var logics = go.GetComponents<AbstractEntityLogic>();

        if (logics.Length == 1)
            return logics[0];

        node = new GameObject("Node").AddComponent<EntityLogicNode>();
        node.transform.parent = go.transform.parent;
        go.transform.parent = node.transform;

        return node;
    }
}

#endif
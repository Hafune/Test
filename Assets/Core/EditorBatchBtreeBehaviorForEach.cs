#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Editor;
using BehaviorDesigner.Runtime;
using Core.BehaviorTree.Scripts;
using Core.BehaviorTree.Shared;
using UnityEditor;
using UnityEngine;

public class EditorBatchBtreeBehaviorForEach : AbstractEditorBatchExecute
{
    public ExternalBehavior Behavior;

    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        foreach (var i in suitable)
        {
            var bt = i.GetComponent<BehaviorTree>();
            bt.ExternalBehavior = Behavior;
            yield return null;
            var comps = i.GetComponentsInChildren<BTreeNpcAction>(true);

            List<SharedVariable> list = new();

            for (int a = 0, iMax = comps.Length; a < iMax; a++)
            {
                var action = comps[a];
                list.Add(new SharedBTreeNpcAction
                {
                    Name = $"NpcAction{a + 1}",
                    Value = action,
                });
            }

            BehaviorSource behaviorSource = bt.GetBehaviorSource();
            behaviorSource.SetAllVariables(list);

            if (BehaviorDesignerPreferences.GetBool(BDPreferences.BinarySerialization))
                BinarySerialization.Save(behaviorSource);
            else
                JSONSerialization.Save(behaviorSource);

            // var source = bt.GetBehaviorSource();
            // bt.SetBehaviorSource(source);
            // BehaviorDesignerUtility.SetObjectDirty(bt);
            yield return null;
            EditorUtility.SetDirty(i);
        }

        yield return null;

        callback?.Invoke();
    }
}
#endif
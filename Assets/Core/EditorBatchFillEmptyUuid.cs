#if UNITY_EDITOR
using System;
using System.Collections;
using Core;
using UnityEditor;
using UnityEngine;

public class EditorBatchFillEmptyUuid : AbstractEditorBatchExecute
{
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action next, Action cancel)
    {
        foreach (var i in suitable)
        {
            foreach (var spawn in i.GetComponentsInChildren<SpawnPrefabTaskWithOverrides>())
            {
                var instance = spawn.GetComponentInChildren<GemInstance>();

                if (instance is null)
                    continue;

                spawn.transform.rotation = Quaternion.identity;
                EditorUtility.SetDirty(spawn.gameObject);
                
                if (!string.IsNullOrEmpty(instance.InstanceUuid))
                    continue;

                instance.RegenerateInstanceUuid();
                PrefabUtility.RecordPrefabInstancePropertyModifications(instance);
                yield return null;
                spawn.SerializePropertyModifications();
                PrefabUtility.RecordPrefabInstancePropertyModifications(spawn);
                spawn.transform.rotation = Quaternion.identity;
                EditorUtility.SetDirty(spawn.gameObject);
            }
            
            // foreach (var spawn in i.GetComponentsInChildren<SpawnPrefabTaskWithOverrides>())
            // {
            //     var instance = spawn.GetComponentInChildren<GemInstance>();
            //
            //     if (instance is null || !string.IsNullOrEmpty(instance.InstanceUuid))
            //         continue;
            //
            //     instance.RegenerateInstanceUuid();
            //     PrefabUtility.RecordPrefabInstancePropertyModifications(instance);
            //     yield return null;
            //     spawn.SerializePropertyModifications();
            //     PrefabUtility.RecordPrefabInstancePropertyModifications(spawn);
            //     
            //     EditorUtility.SetDirty(spawn.gameObject);
            // }
        }

        yield return null;

        next?.Invoke();
    }
}
#endif
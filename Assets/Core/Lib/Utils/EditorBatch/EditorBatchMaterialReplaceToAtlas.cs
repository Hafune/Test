#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    public class EditorBatchMaterialReplaceToAtlas : AbstractEditorBatchExecute
    {
        [SerializeField] private List<Material> _materialsToReplace;

        protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
        {
            var materials = _materialsToReplace
                .ToDictionary(i => i.name.Replace("_atlas", ""), i => i);

            foreach (var t in suitable)
            foreach (var meshRenderer in t.GetComponentsInChildren<MeshRenderer>())
            {
                if (meshRenderer.sharedMaterial &&
                    materials.TryGetValue(meshRenderer.sharedMaterial.name, out var m))
                {
                    meshRenderer.sharedMaterial = m;
                    EditorUtility.SetDirty(meshRenderer);
                }
            }

            yield return null;

            callback?.Invoke();
        }
    }
}
#endif
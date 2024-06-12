#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Core.Lib
{
    public class EditorMaterialShadowChange : AbstractEditorBatchExecute
    {
        [SerializeField] private List<Material> _materialsWithShadows;

        protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
        {
            foreach (var t in suitable)
            {
                EditorUtility.SetDirty(t);
                foreach (var meshRenderer in t.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.staticShadowCaster = true;
                    meshRenderer.lightProbeUsage = LightProbeUsage.Off;
                    meshRenderer.receiveGI = ReceiveGI.LightProbes;
                }
            }
            
            var materialsWithShadowsIds = _materialsWithShadows.Select(i => i.GetInstanceID());
            
            foreach (var t in suitable)
            foreach (var meshRenderer in t.GetComponentsInChildren<MeshRenderer>())
            {
                if (meshRenderer.sharedMaterial &&
                    !materialsWithShadowsIds.Contains(meshRenderer.sharedMaterial.GetInstanceID()))
                    meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            }

            EditorUtility.SetDirty(this);

            yield return null;
        
            callback?.Invoke();
        }
    }
}
#endif
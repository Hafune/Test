#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    public class EditorBatchParticleFix : AbstractEditorBatchExecute
    {
        protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
        {
            // foreach (var t in suitable)
            // {
            //     var particle = t.GetComponent<ParticleSystem>();
            //     
            //     if (particle != null)
            //         continue;
            //
            //     var newParticle = t.gameObject.AddComponent<ParticleSystem>();
            //     
            //     var newParticleEmission = newParticle.emission;
            //     newParticleEmission.enabled = false;
            //     
            //     var newParticleShape = newParticle.shape;
            //     newParticleShape.enabled = false;
            //
            //     newParticle.GetComponent<ParticleSystemRenderer>().enabled = false;
            // }
            
            foreach (var t in suitable)
            foreach (var particle in t.GetComponentsInChildren<ParticleSystem>())
            {
                // if (particle.transform == particle.transform.root)
                //     continue;
                
                var particleMain = particle.main;
                // particleMain.simulationSpeed = 1;
                // particleMain.stopAction = ParticleSystemStopAction.None;
                particleMain.loop = false;
                    
                EditorUtility.SetDirty(particle);
                
                if (particle.GetComponent<ParticleSystemRenderer>().enabled == false && particle.transform.childCount == 0)
                    DestroyImmediate(particle);
            }

            yield return null;

            callback?.Invoke();
        }
    }
}
#endif
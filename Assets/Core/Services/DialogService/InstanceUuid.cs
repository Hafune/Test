using System;
using UnityEngine;
using VInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib
{
    public class InstanceUuid : MonoBehaviour
    {
        [field: SerializeField] public string uuid { get; private set; }        

#if UNITY_EDITOR
        [Button]
        private void RegenerateInstanceUuid()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;

            uuid = Guid.NewGuid().ToString();
        }
#endif
    }
}
using System;
using Core.Lib;
using Reflex;
using UnityEngine;

namespace Lib
{
    public class UnityComponentMappedPool : IDisposable
    {
        private Glossary<UnityComponentPool<Component>> _prefabPool = new();
        private Context _context;
        private readonly bool _dontDestroyOnLoad;

        public UnityComponentMappedPool(Context context, bool dontDestroyOnLoad = false)
        {
            _context = context;
            _dontDestroyOnLoad = dontDestroyOnLoad;
        }

        public T GetPrefabAndSetParent<T>(T prefab, Transform parent) where T : Component
        {
#if UNITY_EDITOR
            if (!prefab)
                throw new Exception("missing reference префаб");
#endif
            var id = prefab.GetInstanceID();

            if (!_prefabPool.ContainsKey(id))
                _prefabPool.Add(id, new UnityComponentPool<Component>(_context, prefab, _dontDestroyOnLoad));

            return (T)_prefabPool.GetValue(id)
                .GetObject(parent.position, Quaternion.identity, parent);
        }

        public T GetComponentByPrefab<T>(T prefab, Vector3 position, Quaternion quaternion) where T : Component
        {
#if UNITY_EDITOR
            if (!prefab)
                throw new Exception("missing reference префаб");
#endif
            var id = prefab.GetInstanceID();
            
            if (!_prefabPool.ContainsKey(id))
                _prefabPool.Add(id,
                    new UnityComponentPool<Component>(_context, prefab, _dontDestroyOnLoad));

            return (T)_prefabPool.GetValue(id).GetObject(position, quaternion);
        }

        public void ForceReturnInPool()
        {
            foreach (var e in _prefabPool)
                e.Value.ForceReturnInPool();
        }

        public void Dispose()
        {
            foreach (var e in _prefabPool)
                e.Value.Dispose();

            _prefabPool.Clear();
        }
    }
}
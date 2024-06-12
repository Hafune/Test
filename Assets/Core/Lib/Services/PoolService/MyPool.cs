using System;
using Core.Lib;
using Reflex;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lib
{
    public class MyPool
    {
        private PoolFactory _poolFactory;
        private Glossary<object> _prefabPools = new();

        public MyPool(Context myContext, bool dontDestroyOnLoad = false) =>
            _poolFactory = new PoolFactory(myContext, dontDestroyOnLoad);

        public UnityComponentMappedPool BuildMappedPull() => _poolFactory.BuildMappedPull();

        public UnityComponentPool<T> GetPullByPrefab<T>(T prefab) where T : Component
        {
            BuildPoolIfNotExist(prefab);
            return (UnityComponentPool<T>)_prefabPools.GetValue(prefab.GetInstanceID());
        }

        private void BuildPoolIfNotExist<T>(T prefab) where T : Component
        {
#if UNITY_EDITOR
            if (!prefab)
                throw new Exception("missing reference префаб");
#endif

            if (_prefabPools.ContainsKey(prefab.GetInstanceID()))
                return;

            var pool = _poolFactory.BuildPool(prefab);
            Assert.IsNotNull(pool);
            _prefabPools.Add(prefab.GetInstanceID(), pool);
        }

        public void ClearPools()
        {
            _poolFactory.ClearPools();
            _prefabPools.Clear();
        }

        public void ForceDisable() => _poolFactory.ForceReturnInPool();
    }
}
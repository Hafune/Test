using Core.Lib;
using Reflex;
using UnityEngine;

namespace Lib
{
    public class PoolFactory
    {
        private MyList<IPool> _pools = new();
        private MyList<UnityComponentMappedPool> _mappedPools = new();
        private Context _context;
        private readonly bool _dontDestroyOnLoad;

        public PoolFactory(Context context, bool dontDestroyOnLoad = false)
        {
            _context = context;
            _dontDestroyOnLoad = dontDestroyOnLoad;
        }

        public UnityComponentPool<T> BuildPool<T>(T prefab) where T : Component
        {
            var pool = new UnityComponentPool<T>(_context, prefab, _dontDestroyOnLoad);
            _pools.Add(pool);

            return pool;
        }

        public UnityComponentMappedPool BuildMappedPull()
        {
            var pool = new UnityComponentMappedPool(_context, _dontDestroyOnLoad);
            _mappedPools.Add(pool);
            return pool;
        }

        public void ClearPools()
        {
            foreach (var pool in _pools)
                pool.Dispose();
            
            foreach (var pool in _mappedPools)
                pool.Dispose();
        }

        public void ForceReturnInPool()
        {
            foreach (var pool in _pools)
                pool.ForceReturnInPool();
            
            foreach (var pool in _mappedPools)
                pool.ForceReturnInPool();
        }
    }
}
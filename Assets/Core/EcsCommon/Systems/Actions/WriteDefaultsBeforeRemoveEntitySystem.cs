using System;
using System.Collections.Generic;
using System.Reflection;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;

namespace Core.Systems
{
    public class WriteDefaultsBeforeRemoveEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                TransformComponent,
                WriteDefaultsBeforeRemoveEntityComponent,
                EventRemoveEntity
            >> _filter;

        private readonly EcsPoolInject<TransformComponent> _transformPool;
        private readonly EcsPoolInject<WriteDefaultsBeforeRemoveEntityComponent> _pool;

#if UNITY_EDITOR
        private HashSet<MethodInfo> _debugDuplicationCheck = new();
#endif
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                if (!_transformPool.Value.Get(i).transform.IsDestroyed())
                {
#if UNITY_EDITOR
                    if (_pool.Value.Get(i).writeDefaults is not null)
                        foreach (var action in _pool.Value.Get(i).writeDefaults.GetInvocationList())
                            if (!_debugDuplicationCheck.Add(action.Method))
                                throw new Exception(
                                    $"Один и тот же метод был добавлен дважды!: {action.Target.GetType().Name} {action.Method.Name}");

                    _debugDuplicationCheck.Clear();
#endif
                    _pool.Value.Get(i).writeDefaults?.Invoke(i);

                }
        }
    }
}
using System;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ScaleValueSystem<T> : IEcsRunSystem
        where T : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                T,
                EventValueUpdated<T>,
                TransformComponent,
                EnemyTag
            >> _filterBase;

        private readonly EcsPoolInject<T> _valuePool;
        private readonly EcsPoolInject<BaseValueComponent<T>> _basePool;
        private readonly EcsPoolInject<TransformComponent> _transformPool;

        private readonly Func<int, float> _getScale;

        public ScaleValueSystem(Func<int, float> getScale) => _getScale = getScale;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filterBase.Value)
                _valuePool.Value.Get(i).value += _basePool.Value.Get(i).baseValue *
                                                _getScale.Invoke(_transformPool.Value.Get(i).convertToEntity
                                                    .TemplateId);
        }
    }
}
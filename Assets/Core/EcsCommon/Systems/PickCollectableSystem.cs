using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class PickCollectableSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                CollectableAreaComponent,
                MagnetAreaComponent,
                ActiveArea<CollectableAreaComponent>,
                RigidbodyComponent
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                ReceiverClientComponent
            >> _itemsStackFilter;

        private readonly EcsFilterInject<
            Inc<
                NodeComponent,
                ParentComponent,
                TransformComponent,
                StackComponent
            >> _stackFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            var targetEntity = _pools.CollectableArea.Get(entity).area.GetFirst();
#if UNITY_EDITOR
            if (!_itemsStackFilter.Value.HasEntity(targetEntity))
                throw new Exception("Нет необходимых компонентов!!!");
#endif
            _pools.CollectableArea.Del(entity);
            _pools.MagnetArea.Del(entity);
            _pools.Magnet.Del(entity);

            var node = _pools.Node.GetOrInitialize(targetEntity);

            if (node.children.Count == 0)
            {
                node.children.Add(entity);
                _pools.Parent.Add(entity).entity = targetEntity;
                _pools.Stack.Add(entity).targetTransform =
                    _pools.ReceiverClient.Get(targetEntity).receiverClient.transform;
                _pools.Node.AddIfNotExist(entity);
            }
            else
            {
                var tailEntity = node.children.Items[0];

                while (_stackFilter.Value.HasEntity(tailEntity) &&
                       (node = _pools.Node.Get(tailEntity)).children.Count != 0)
                    tailEntity = node.children.Items[0];

                node.children.Add(entity);
                _pools.Parent.Add(entity).entity = tailEntity;
                _pools.Stack.Add(entity).targetTransform = _pools.Transform.Get(tailEntity).transform;
                _pools.Node.AddIfNotExist(entity);
            }
        }
    }
}
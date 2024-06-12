using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Lib;
using Voody.UniLeo.Lite;

namespace Core.Components
{
    //постораться заменить на связи радителя и ребенка (NodeComponent и ParentComponent)
    public struct NestedEntitiesComponent : IEcsAutoReset<NestedEntitiesComponent>, IResetInProvider
    {
        private HashSet<EcsPackedEntityWithWorld> _nestedEntities;
        private Dictionary<IEcsPool, object> _syncComponents;

        public void AutoReset(ref NestedEntitiesComponent c)
        {
            if (c._nestedEntities is null)
            {
                c._nestedEntities = new();
                c._syncComponents = new();
            }

            c._nestedEntities.Clear();
            c._syncComponents.Clear();
        }

        public void ForEachEntities(Action<int> callback)
        {
            bool hasRemovedEntity = false;

            foreach (var packedEntity in _nestedEntities)
            {
                if (packedEntity.Unpack(out _, out int entity))
                    callback.Invoke(entity);
                else
                    hasRemovedEntity = true;
            }

            if (hasRemovedEntity)
                _nestedEntities.RemoveWhere(e => !e.Unpack(out _, out _));
        }

        public bool AddNestedEntity(EcsPackedEntityWithWorld packedEntity) => _nestedEntities.Add(packedEntity);

        public void AddComponent<T>(T component, EcsPool<T> pool) where T : struct => _syncComponents[pool] = component;

        public void RemoveComponent(IEcsPool pool) => _syncComponents.Remove(pool);

        public void SetupComponents(int newEntity)
        {
            foreach (var pair in _syncComponents)
                if (!pair.Key.Has(newEntity))
                    pair.Key.AddRaw(newEntity, pair.Value);
                else
                    pair.Key.SetRaw(newEntity, pair.Value);
        }

        public void CopySavedComponentsFrom(NestedEntitiesComponent nestedComponent)
        {
            _syncComponents.Clear();
            _syncComponents.AddRange(nestedComponent._syncComponents);
        }
    }
}
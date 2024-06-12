using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class DamageOrbSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                DamageOrbSlotTag,
                DamageOrbValueComponent,
                DamageOrbPrefabComponent,
                TransformCenterComponent
            >,
            Exc<
                DamageOrbParentComponent,
                InProgressTag<ActionDeathComponent>
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                DamageOrbParentComponent,
                DamageOrbValueComponent,
                DamageOrbPrefabComponent,
                TransformCenterComponent
            >,
            Exc<EventDeath>> _progressFilter;

        private readonly EcsFilterInject<
            Inc<
                DamageOrbParentComponent
            >,
            Exc<DamageOrbSlotTag>> _progressEndFilter;

        private readonly EcsFilterInject<
            Inc<
                EventDeath,
                DamageOrbParentComponent
            >> _progressEndByDeathFilter;

        private readonly ComponentPools _pools;
        private readonly ChildEntityBuilder _childEntityBuilder;
        private const float RotationPerSecond = .5f;

        public DamageOrbSpawnSystem(Context context) => _childEntityBuilder = new(context);

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pools.DamageOrbParent.Add(i);

            foreach (var i in _progressFilter.Value)
            {
                float value = _pools.DamageOrbValue.Get(i).value;
                ref var orbParent = ref _pools.DamageOrbParent.Get(i);

                if (orbParent.orbsStates.Count != value)
                {
                    orbParent.time = 0;
                    orbParent.orbIndex = 0;
                    orbParent.orbsStates.Clear();

                    while (orbParent.orbsStates.Count < value)
                    {
                        orbParent.orbsStates.Add(-1);
                        orbParent.orbEntities.Add(-1);
                    }

                    foreach (var child in orbParent.orbEntities)
                        ManualRemoveChild(child, orbParent);

                    while (orbParent.orbsStates.Count > value)
                    {
                        orbParent.orbsStates.RemoveAt((int)value);
                        orbParent.orbEntities.RemoveAt((int)value);
                    }
                }

                orbParent.time += Time.deltaTime;
                float spawnDelay = 1 / RotationPerSecond / value;

                if (orbParent.time < spawnDelay)
                    continue;

                orbParent.time -= spawnDelay;
                orbParent.orbIndex = (orbParent.orbIndex + 1) % (int)value;
                var states = orbParent.orbsStates;
                int cycle = states[orbParent.orbIndex] = Math.Min(2, states[orbParent.orbIndex] + 1);

                if (cycle != 1)
                    continue;

                var prefab = _pools.DamageOrbPrefab.Get(i).prefab;
                var center = _pools.TransformCenter.Get(i).transform;
                var data = _childEntityBuilder.BuildEvent(
                    prefab,
                    center,
                    i,
                    OnBuild,
                    OnRemove);

                data.AddComponent(new TransformCenterComponent { transform = center }, _pools.TransformCenter);
                data.AddComponent(new DamageOrbComponent
                    {
                        index = orbParent.orbIndex,
                        rotationProgress = 0,
                        rotationPerSecond = RotationPerSecond
                    },
                    _pools.DamageOrb);
            }

            foreach (var i in _progressEndFilter.Value)
            {
                var orbParent = _pools.DamageOrbParent.Get(i);
                foreach (var child in orbParent.orbEntities)
                    ManualRemoveChild(child, orbParent);

                _pools.DamageOrbParent.Del(i);
            }

            foreach (var i in _progressEndByDeathFilter.Value)
            {
                var orbParent = _pools.DamageOrbParent.Get(i);
                foreach (var child in orbParent.orbEntities)
                    ManualRemoveChild(child, orbParent);

                _pools.DamageOrbParent.Del(i);
            }
        }

        private void ManualRemoveChild(int child, DamageOrbParentComponent orbParent)
        {
            if (child == -1)
                return;

            _pools.EventRemoveEntity.Add(child);
            orbParent.orbEntities[_pools.DamageOrb.Get(child).index] = -1;
            _pools.Parent.Get(child).OnRemove -= OnRemove;
        }

        private void OnBuild(int parent, int i) =>
            _pools.DamageOrbParent.Get(parent).orbEntities[_pools.DamageOrb.Get(i).index] = i;

        private void OnRemove(int parent, int i)
        {
            var orbTag = _pools.DamageOrbParent.Get(parent);
            var index = _pools.DamageOrb.Get(i).index;
            orbTag.orbEntities[index] = -1;
            orbTag.orbsStates[index] = -1;
        }
    }
}
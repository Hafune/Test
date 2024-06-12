using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.Systems
{
    public class ChildEntityBuilder
    {
        private readonly EcsPool<EventBuildEntity> _eventBuildEntityPool;

        public ChildEntityBuilder(Context context)
        {
            var pools = context.Resolve<ComponentPools>();
            _eventBuildEntityPool = pools.EventBuildEntity;
        }

        public BuildEntityData BuildEvent(ConvertToEntity prefab,
            Transform emitter,
            int parent,
            Action<int, int> onBuild = null,
            Action<int, int> onRemove = null)
        {
            var data = BuildEntityData.GetPooled();
            data.prefab = prefab;
            data.emitter = emitter;
            data.OnBuild = onBuild;
            data.OnRemove = onRemove;

            _eventBuildEntityPool.GetOrInitialize(parent).list.Add(data);
            return data;
        }
    }
}
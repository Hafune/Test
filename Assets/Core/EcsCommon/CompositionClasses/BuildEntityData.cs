using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.Components
{
    public class BuildEntityData
    {
        private static readonly Stack<BuildEntityData> _pool = new();

        public static BuildEntityData GetPooled()
        {
            if (!_pool.TryPop(out var payload))
                return new BuildEntityData();

            return payload;
        }

        public static void ReturnInPoll(BuildEntityData data) => _pool.Push(data);

        public ConvertToEntity prefab;
        public Transform emitter;
        /// <summary>
        /// parentEntity, childEntity
        /// </summary>
        public Action<int, int> OnBuild;
        /// <summary>
        /// parentEntity, childEntity
        /// </summary>
        public Action<int, int> OnRemove;

        private Dictionary<IEcsPool, object> _entityComponents;

        private BuildEntityData() => Reset();

        public void Reset()
        {
            prefab = null;

            _entityComponents ??= new Dictionary<IEcsPool, object>();
            _entityComponents.Clear();
        }

        public void AddComponent<T>(T component, EcsPool<T> pool) where T : struct =>
            _entityComponents[pool] = component;

        public int BuildEntity(Func<ConvertToEntity, Vector3, Quaternion, ConvertToEntity> pool)
        {
            var convertToEntity = pool.Invoke(prefab, emitter.position, emitter.rotation);
            convertToEntity.ManualConnection();
            int entity = convertToEntity.RawEntity;

            foreach (var pair in _entityComponents)
                pair.Key.AddRaw(entity, pair.Value);

            return entity;
        }

        public void CopyFrom(BuildEntityData src)
        {
            prefab = src.prefab;
            emitter = src.emitter;
            _entityComponents.Clear();
            _entityComponents.AddRange(src._entityComponents);
        }
    }
}
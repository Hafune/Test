using System;
using System.Collections.Generic;
using Core.Components;
using Core.Lib;
using Core.Systems;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace Core
{
    public class WorldMessages
    {
        private EcsWorld _world;
        private Dictionary<Type, HashSet<Type>> _existingSystemTypes = new();
        private readonly IEcsEngine _ecsEngine;

        public WorldMessages(EcsWorld world, IEcsEngine ecsEngine)
        {
            _world = world;
            _ecsEngine = ecsEngine;
        }

        public Builder<T> BuildUiEntityWithLink<T>()
            where T : struct
        {
            var entity = _world.NewEntity();
            _world.GetPool<GlobalUiLink<T>>().Add(entity);

            return new Builder<T>(entity, _existingSystemTypes, _ecsEngine, _world);
        }

        private void Add<T>(T value, int entity, bool replaceIfExist = false) where T : struct
        {
            var pool = _world.GetPool<T>();

            if (value is IResetInProvider and IEcsAutoReset<T> auto)
            {
                auto.AutoReset(ref value);

                if (replaceIfExist && pool.Has(entity))
                    pool.Get(entity) = value;
                else
                    pool.Add(entity) = value;
            }
            else
            {
                if (replaceIfExist && pool.Has(entity))
                    pool.Get(entity) = value;
                else
                    pool.Add(entity) = value;
            }
        }

        public class Builder<T> where T : struct
        {
            private Dictionary<Type, HashSet<Type>> _existingSystemTypes;
            private IEcsEngine _ecsEngine;
            private EcsWorld _world;
            
            public int RawEntity { get; }

            public Builder(
                int entity,
                Dictionary<Type, HashSet<Type>> existingSystemTypes,
                IEcsEngine ecsEngine,
                EcsWorld world
            )
            {
                RawEntity = entity;
                _existingSystemTypes = existingSystemTypes;
                _ecsEngine = ecsEngine;
                _world = world;
            }

            public Builder<T> Add<T1>(Action<float> refreshFunction) where T1 : struct, IValue
            {
                _world.GetPool<UiValue<T1>>().Add(RawEntity).data = new UiFloat(refreshFunction);;
                return AddFloat<T1>();
            }

            public Builder<T> Add<T1>(Action<string> refreshFunction) where T1 : struct, IStringValue
            {
                _world.GetPool<UiStringValue<T1>>().Add(RawEntity) = UiString.BuildUiValueBy<T1>(refreshFunction);
                return AddString<T1>();
            }

            private Builder<T> AddFloat<T1>() where T1 : struct, IValue
            {
                var type0 = typeof(T);
                var type1 = typeof(T1);

                if (!_existingSystemTypes.ContainsKey(type0))
                    _existingSystemTypes.Add(type0, new HashSet<Type>());

                if (_existingSystemTypes[type0].Contains(type1))
                    return this;

                _existingSystemTypes[type0].Add(type1);
                _ecsEngine.AddUiSystem(new UpdateGlobalUiValueSystem<T, T1>());

                return this;
            }

            private Builder<T> AddString<T1>() where T1 : struct, IStringValue
            {
                var type0 = typeof(T);
                var type1 = typeof(T1);

                if (!_existingSystemTypes.ContainsKey(type0))
                    _existingSystemTypes.Add(type0, new HashSet<Type>());

                if (_existingSystemTypes[type0].Contains(type1))
                    return this;

                _existingSystemTypes[type0].Add(type1);
                _ecsEngine.AddUiSystem(new UpdateGlobalUiStringValueSystem<T, T1>());

                return this;
            }
        }
    }
}
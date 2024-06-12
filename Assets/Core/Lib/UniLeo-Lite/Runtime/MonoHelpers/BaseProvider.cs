using Leopotam.EcsLite;
using Lib;
using Voody.UniLeo.Lite;

namespace Core.Lib
{
    public class BaseProvider<T> : IBaseProvider where T : struct
    {
        private enum ValueType
        {
            Unknown,
            Auto,
            Simple,
        }
        
        private T value;
        private EcsPool<T> _pool;
        private IEcsAutoReset<T> _iValue;
        private ValueType valueType = ValueType.Unknown;

        public BaseProvider(T v) => value = v;

        public override void Attach(int entity, EcsWorld world, bool addOrReplace)
        {
            if (valueType == ValueType.Unknown)
                Init(world);

            if (valueType == ValueType.Auto)
                _iValue.AutoReset(ref value);

            if (addOrReplace)
                _pool.GetOrInitialize(entity) = value;
            else
                _pool.Add(entity) = value;
        }

        public override void Remove(int entity, EcsWorld world)
        {
            if (valueType == ValueType.Unknown)
                Init(world);

            _pool.DelIfExist(entity);
        }

        public void Init(EcsWorld world)
        {
            _pool = world.GetPool<T>();

            if (value is IResetInProvider and IEcsAutoReset<T> iValue)
            {
                _iValue = iValue;
                valueType = ValueType.Auto;
            }
            else
            {
                valueType = ValueType.Simple;
            }
        }
    }
    
    public abstract class IBaseProvider
    {
        public abstract void Attach(int entity, EcsWorld world, bool addOrReplace);
        public abstract void Remove(int entity, EcsWorld world);
    }
}
using BehaviorDesigner.Runtime.Tasks;
using Leopotam.EcsLite;
using Reflex;
using Voody.UniLeo.Lite;

namespace Core.BehaviorTree
{
    public abstract class AbstractEntityCondition : Conditional
    {
        protected ConvertToEntity ConvertToEntity { get; private set; }
        protected EcsWorld World { get; private set; }
        protected EcsEngine EcsEngine { get; private set; }
        protected int RawEntity => ConvertToEntity.RawEntity;
        protected Context Context => ConvertToEntity.Context;

        public override void OnAwake()
        {
            ConvertToEntity = GetComponent<ConvertToEntity>();
            World = ConvertToEntity.Context.Resolve<EcsWorld>();
            EcsEngine = ConvertToEntity.Context.Resolve<EcsEngine>();
        }
        
        protected EcsPool<T> GetPool<T>() where T : struct => World.GetPool<T>();
    }
}
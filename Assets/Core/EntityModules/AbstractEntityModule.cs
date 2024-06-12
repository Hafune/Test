using Lib;
using Reflex;

namespace Core.EntityModules
{
    public abstract class AbstractEntityModule : MonoConstruct
    {
        protected Context Context { get; private set; }

        protected override void Construct(Context context) => Context = context;

        public virtual void SetupLogic(int entity){}
        
        public virtual void StartLogic(int entity){}
        public virtual void UpdateLogic(int entity){}
        public virtual void CancelLogic(int entity){}
        
        public virtual void RemoveLogic(int entity){}
    }
}
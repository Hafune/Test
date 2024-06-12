using Lib;
using Reflex;

namespace Core.Systems
{
    public abstract class AbstractActionEntityLogic : MonoConstruct
    {
        protected Context Context { get; private set; }

        protected override void Construct(Context context) => Context = context;

        // @formatter:off
        public virtual bool CheckConditionLogic(int entity) => true;
        
        public virtual void ResetOnStart(int entity){}
        public virtual void StartLogic(int entity){}

        public virtual void UpdateLogic(int entity){}
        
        public virtual void CompleteStreamingLogic(int entity){}

        public virtual void CancelLogic(int entity){}
    }
}
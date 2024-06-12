using Lib;
using Reflex;

namespace Core.Systems
{
    public abstract class AbstractEntityLogic : MonoConstruct
    {
        protected Context Context { get; private set; }

        protected override void Construct(Context context) => Context = context;

        public abstract void Run(int entity);
    }
}
using Lib;
using Reflex;

namespace Core.Lib
{
    public abstract class AbstractEffect : MonoConstruct
    {
        protected Context Context { get; private set; }

        protected override void Construct(Context context) => Context = context;

        public abstract void Execute();
    }
}
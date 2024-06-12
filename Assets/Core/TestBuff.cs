using System.Collections;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using Voody.UniLeo.Lite;

namespace Core
{
    public class TestBuff : MonoConstruct
    {
        private Context _context;

        protected override void Construct(Context context) => _context = context;

        private IEnumerator Start()
        {
            yield return null;
            var world = _context.Resolve<EcsWorld>();
            var filter = world.Filter<HitPointValueComponent>().End();
            var pools = _context.Resolve<ComponentPools>();
            foreach (var i in filter)
            {
                pools.HitPointValue.Get(i).value = 100000;
                pools.EventUpdatedHitPointValue.AddIfNotExist(i);
                AddComponents(i);
            }
        }

        private void AddComponents(int entity)
        {
            var world = _context.Resolve<EcsWorld>();

            foreach (var provider in GetComponents<BaseMonoProvider>())
                provider.Attach(entity, world, true);
        }
    }
}
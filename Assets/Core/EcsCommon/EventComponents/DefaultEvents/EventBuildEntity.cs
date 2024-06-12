using Core.Lib;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace Core.Components
{
    [MyDoc("Команда создать чилда")]
    public struct EventBuildEntity : IEcsAutoReset<EventBuildEntity>, IResetInProvider
    {
        public MyList<BuildEntityData> list { get; private set; }

        public void AutoReset(ref EventBuildEntity c)
        {
            c.list ??= new();
            for (int i = 0,iMax = c.list.Count; i < iMax; i++)
            {
                var data = c.list.Items[i];
                data.Reset();
                BuildEntityData.ReturnInPoll(data);
            }
            
            c.list.Clear();
        }
    }
}
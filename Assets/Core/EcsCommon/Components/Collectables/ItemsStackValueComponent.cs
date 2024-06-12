using System;
using Core.Lib;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace Core.Components
{
    [Serializable]
    public struct ItemsStackValueComponent : IEcsAutoReset<ItemsStackValueComponent>, IResetInProvider
    {
        public MyList<int> items;
        public ItemsStackClient itemsStackClient;

        public void AutoReset(ref ItemsStackValueComponent c)
        {
            c.items ??= new();
            c.items.Clear();
        }
    }
}
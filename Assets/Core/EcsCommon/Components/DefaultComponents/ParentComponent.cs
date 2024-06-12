using System;
using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct ParentComponent
    {
        public int entity;
        /// <summary>
        /// parentEntity, childEntity
        /// </summary>
        public Action<int, int> OnRemove;
    }

    public struct NodeComponent : IEcsAutoReset<NodeComponent>
    {
        public MyList<int> children;

        public void AutoReset(ref NodeComponent c)
        {
            c.children ??= new();
            c.children.Clear();
        }
    }
}
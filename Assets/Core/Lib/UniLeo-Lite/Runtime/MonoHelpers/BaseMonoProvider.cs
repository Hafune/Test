using Leopotam.EcsLite;
using UnityEngine;

namespace Voody.UniLeo.Lite
{
    public abstract class BaseMonoProvider : MonoBehaviour
    {
        public abstract void Attach(int entity, EcsWorld world, bool addOrReplace);
        public abstract void Remove(int entity, EcsWorld world);
    }
}
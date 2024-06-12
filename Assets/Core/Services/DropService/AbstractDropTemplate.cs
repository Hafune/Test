using UnityEngine;

namespace Core.Services
{
    public abstract class AbstractDropTemplate : ScriptableObject
    {
        public abstract bool Contains(int id);
        public abstract void SpawnDrop(Vector3 position, float chanceScale, ItemsService itemsService);
    }
}
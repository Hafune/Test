using System;
using Core.EcsCommon.ValueComponents;
using Core.Services;
using Reflex;
using Reflex.Injectors;
using UnityEngine;
using VInspector;

namespace Core.Lib
{
    public class TaskTestSpawnItem : MonoBehaviour, IMyTask
    {
        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            var itemsService = context.Resolve<ItemsService>();

            foreach (ItemDatabaseEnum item in Enum.GetValues(typeof(ItemDatabaseEnum)))
                itemsService.SpawnItem(transform.position + 1 * Vector3.right, item);

            onComplete?.Invoke(this);
        }

#if UNITY_EDITOR
        [Button]
        private void Run()
        {
            var context = EditorContextAccess.context;
            var itemsService = context.Resolve<ItemsService>();

            foreach (ItemDatabaseEnum item in Enum.GetValues(typeof(ItemDatabaseEnum)))
            {
                if (!MyEnumUtility<ItemDatabaseEnum>.Name((int)item).Contains("5_"))
                    continue;
                
                itemsService.SpawnItem(transform.position + 1 * Vector3.right, item);
            }
        }
#endif
    }
}
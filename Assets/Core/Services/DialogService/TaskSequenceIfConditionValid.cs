using System;
using System.Collections.Generic;
using Core.EcsCommon.ValueComponents;
using Core.Lib.MyTasks;
using Lib;
using Reflex;
using UnityEngine;

namespace Core
{
    public class TaskSequenceIfConditionValid : MonoBehaviour, IMyTask
    {
        private enum TypeEnum
        {
            All,
            Any
        }

        [field: SerializeField] private TypeEnum _type = TypeEnum.All;
        private TaskSequenceCore _tasks;

        private void OnValidate() => gameObject.Rename($"Sequence_If_Condition_" + MyEnumUtility<TypeEnum>.Name((int)_type));

        public bool InProgress => _tasks?.InProgress ?? false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            var c = payload.Get<Conditions>();
            if (_type == TypeEnum.All && !c.All() ||
                _type == TypeEnum.Any && !c.Any())
            {
                onComplete?.Invoke(this);
                return;
            }

            List<IMyTask> iTasks = new(transform.childCount);
            transform.ForEachSelfChildren<IMyTask>(task => iTasks.Add(task));
            _tasks = new(iTasks.ToArray());

            _tasks.Begin(context, payload, onComplete);
        }
    }
}
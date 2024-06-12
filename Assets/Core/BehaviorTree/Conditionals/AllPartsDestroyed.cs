using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class AllPartsDestroyed : AbstractEntityCondition
    {
        public ConvertToEntity[] parts;
        private int _totalDestroyedParts;
        private bool _callbacksAdded;
        private bool _success;
        private int count;
        private EcsPool<InProgressTag<ActionDeathComponent>> _dyingWatchPool;

        public override void OnAwake()
        {
            base.OnAwake();
            _dyingWatchPool = World.GetPool<InProgressTag<ActionDeathComponent>>();
        }

        public override void OnStart()
        {
            if (_callbacksAdded)
                return;

            _totalDestroyedParts = 0;
            _success = false;

            for (int i = 0; i < parts.Length; i++)
                if (parts[i].RawEntity == -1 || _dyingWatchPool.Has(parts[i].RawEntity))
                    _totalDestroyedParts++;
                else
                    parts[i].OnEntityWasDeleted += OnPartDestroyed;

            _success = _totalDestroyedParts == parts.Length;
            _callbacksAdded = true;
        }

        public override TaskStatus OnUpdate() => _success ? TaskStatus.Success : TaskStatus.Failure;

        private void OnPartDestroyed(ConvertToEntity convertToEntity)
        {
            
            _totalDestroyedParts++;
            _success = _totalDestroyedParts == parts.Length;
        }

        public override void OnBehaviorComplete() => _callbacksAdded = false;
    }
}
using BehaviorDesigner.Runtime.Tasks;
using Core.Services;
using Voody.UniLeo.Lite;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    [TaskDescription(
        "Wait a specified amount of time. The task will return running until the task is done waiting. It will return success after the wait time has elapsed.")]
    [TaskIcon("{SkinColor}WaitIcon.png")]
    public class AfterActionDelay : Wait
    {
        private BTreeActionTimingsService _service;

        public override void OnAwake()
        {
            base.OnAwake();
            _service ??= GetComponent<ConvertToEntity>().Context.Resolve<BTreeActionTimingsService>();
        }

        public override void OnStart()
        {
            base.OnStart();
            waitDuration *= _service.GetAfterActionDelayScale();
        }
    }
}
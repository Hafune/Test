using UnityEngine;

namespace Core.BehaviorTree.Scripts
{
    public class BTreeCommonNpcAction : BTreeNpcAction
    {
        [field: SerializeField] public float StartDelay { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float WaitTime { get; private set; }
    }
}
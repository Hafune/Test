using UnityEngine;

namespace Core.Services
{
    [CreateAssetMenu(menuName = "Game Config/" + nameof(BTreeActionTimingData))]
    public class BTreeActionTimingData : ScriptableObject
    {
        [field: SerializeField] public float AfterActionDelayScale { get; private set; } = 1f;
        [field: SerializeField] public float CooldownScale { get; private set; } = 1f;
    }
}
using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Config/" + nameof(CommonEffectsTemplate))]
    public class CommonEffectsTemplate : ScriptableObject
    {
        [field: SerializeField] public EffectWithText DamageTextEffectPrefab { get; private set; }
        [field: SerializeField] public EffectWithText CriticalDamageTextEffectPrefab { get; private set; }
        [field: SerializeField] public Transform HitToEnemyShieldEffPrefab { get; private set; }
        [field: SerializeField] public Transform HitToPlayerShieldEffPrefab { get; private set; }
    }
}
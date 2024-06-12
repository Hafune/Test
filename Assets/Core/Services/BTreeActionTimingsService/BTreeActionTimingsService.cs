using UnityEngine;

namespace Core.Services
{
    public class BTreeActionTimingsService : MonoBehaviour
    {
        [SerializeField] private BTreeActionTimingData _data;

        public float GetAfterActionDelayScale() => _data.AfterActionDelayScale;
        public float GetCooldownScale() => _data.CooldownScale;
    }
}
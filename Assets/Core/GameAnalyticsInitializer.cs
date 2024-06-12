using GameAnalyticsSDK;
using UnityEngine;

namespace Core.Lib
{
    public class GameAnalyticsInitializer : MonoBehaviour, IInitializeCheck
    {
        public bool IsInitialized { get; private set; }

        private void Start()
        {
            GameAnalytics.Initialize();
            IsInitialized = true;
            Debug.Log("GameAnalyticsInitializer ended");
        }
    }
}
using AOT;
using System;
using System.Runtime.InteropServices;

namespace Agava.VKGames
{
    public static class BannerAd
    {
        private static Action s_onRewardedCallback;
        private static Action s_onErrorCallback;

        public static void Show(Action onRewardedCallback = null, Action onErrorCallback = null)
        {
            s_onRewardedCallback = onRewardedCallback;
            s_onErrorCallback = onErrorCallback;

            ShowBannerAds(OnOpenCallback, OnErrorCallback);
        }
        
        [DllImport("__Internal")]
        private static extern void ShowBannerAds(Action openCallback, Action errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnOpenCallback()
        {
            s_onRewardedCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnErrorCallback()
        {
            s_onErrorCallback?.Invoke();
        }
    }
}
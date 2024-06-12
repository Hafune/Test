using System;
using System.Collections;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core
{
    public class AddressableService : MonoConstruct
    {
        public event Action OnNextSceneWillBeLoaded;
        public event Action<float> OnLoadingPercentChange;
        public event Action OnSceneLoaded;
        public event Action OnFadeBegin;

        // private AsyncOperationHandle<SceneInstance> _handle;
        private AsyncOperation _handle;

        private Context _context;
        private DarkScreenService _darkScreenService;

        // public long TotalLoadingValue { get; private set; }
        public string SceneName { get; private set; }

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _darkScreenService = _context.Resolve<DarkScreenService>();
        }

        public void LoadSceneAsync(string sceneName, bool immediate = false)
        {
            SceneName = sceneName;
            OnFadeBegin?.Invoke();
            
            switch (immediate)
            {
                case false:
                    _darkScreenService.FadeIn(Begin);
                    break;
                case true:
                    Begin();
                    break;
            }
        }

        private void Begin() => StartCoroutine(LoadSceneAsyncPrivate(SceneName));

        private IEnumerator LoadSceneAsyncPrivate(string sceneName)
        {
            _handle = SceneManager.LoadSceneAsync(sceneName);
            // _handle = Addressables.LoadSceneAsync(sceneName);
            // TotalLoadingValue = 0;
            OnNextSceneWillBeLoaded?.Invoke();

            while (!_handle.isDone)
            {
                yield return null;

                // if (TotalLoadingValue == 0)
                //     TotalLoadingValue = 1;

                OnLoadingPercentChange?.Invoke(_handle.progress);
            }

            OnLoadingPercentChange?.Invoke(_handle.progress);

            // while (_handle.Status == AsyncOperationStatus.None)
            // {
            //     yield return null;
            //
            //     if (TotalLoadingValue == 0)
            //         TotalLoadingValue = _handle.GetDownloadStatus().TotalBytes;
            //
            //     OnLoadingPercentChange?.Invoke(_handle.PercentComplete);
            // }
            //
            // OnLoadingPercentChange?.Invoke(_handle.PercentComplete);
            OnSceneLoaded?.Invoke();
        }
    }
}
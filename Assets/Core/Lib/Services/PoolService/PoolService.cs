using Lib;
using Reflex;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    public class PoolService : IInitializableService
    {
        public MyPool ScenePool { get; private set; }
        public MyPool DontDisposablePool { get; private set; }

        public void InitializeService(Context context)
        {
            ScenePool = new MyPool(context);
            DontDisposablePool = new MyPool(context, true);

            // context.Resolve<AddressableService>().OnNextSceneWillBeLoaded+=;
            SceneManager.activeSceneChanged += (_,_) => ClearPools();
        }

        private void ClearPools()
        {
            ScenePool.ClearPools();
            DontDisposablePool.ForceDisable();
        }
    }
}
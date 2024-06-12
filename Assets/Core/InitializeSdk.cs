using System.Collections;
using Core;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;

public class InitializeSdk : MonoConstruct, IInitializeCheck
{
    private Context _context;

    public bool IsInitialized { get; private set; }

    protected override void Construct(Context context) => _context = context;

    private IEnumerator Start()
    {
        var skdController = _context.Resolve<SdkService>();
        yield return skdController.Initialize(AfterSdkInit);
    }

    private void AfterSdkInit()
    {
        Debug.Log("Sdk loaded");
#if VK_GAMES
        try
        {
            if (!Application.isMobilePlatform)
                Agava.VKGames.BannerAd.Show();
        }
        catch (System.Exception)
        {
            // ignored
        }
#endif
        var data = _context.Resolve<PlayerDataService>();
        data.Initialize(() => IsInitialized = true);
    }
}
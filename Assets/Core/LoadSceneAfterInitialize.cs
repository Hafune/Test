using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Lib;
using Reflex;
using UnityEngine;

public class LoadSceneAfterInitialize : MonoConstruct
{
    [SerializeField] private SceneField _sceneField;
    [SerializeField] private List<MonoBehaviour> _awaitList;

    private Context _context;

    protected override void Construct(Context context) => _context = context;

    private void OnValidate()
    {
        HashSet<MonoBehaviour> set = new();
        _awaitList = _awaitList.Select(item => item is IInitializeCheck && set.Add(item) ? item : null).ToList();
    }

    private IEnumerator Start()
    {
        while (!_awaitList.All(i => ((IInitializeCheck)i).IsInitialized))
            yield return null;

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

        _context.Resolve<AddressableService>().LoadSceneAsync(_sceneField.SceneName);
    }
}
using Core;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapStartPosition : MonoConstruct
{
    [SerializeField] private bool _dontSaveProgress;
    private Context _context;

    protected override void Construct(Context context) => _context = context;

    private void Awake()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var position = transform.position;
        _context.Resolve<GlobalTeleportService>().SetStartPosition(sceneName, position);
        
        if (_dontSaveProgress)
            return;
        
        _context.Resolve<SceneCheckpointsService>().SetPositionAsCheckpointIfEmpty(sceneName, position);
    }
}
using Reflex.Scripts.Core;
using UnityEngine;

namespace Core.Installers
{
    public class TestSceneLauncher : MonoBehaviour
    {
        [SerializeField] private ProjectInstaller _projectInstaller;
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private TaskSequence _flow;

        private void Awake()
        {
            if (_projectInstaller.IsInitialized)
                Run();
            else
                _projectInstaller.OnInitComplete += Run;
        }

        private void Run()
        {
            _sceneContext.gameObject.SetActive(true);

            if (_flow)
                _flow.gameObject.SetActive(true);
        }
    }
}
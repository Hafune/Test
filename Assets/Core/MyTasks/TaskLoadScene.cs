using System;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.SceneManagement;
using VInspector;

namespace Core.Lib
{
    [Obsolete("Эта задача должна быть последней в цепочке!")]
    public class TaskLoadScene : MonoBehaviour, IMyTask
    {
        [SerializeField] private SceneField _scene;

        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            onComplete?.Invoke(this);
            context.Resolve<AddressableService>().LoadSceneAsync(_scene);
        }

        [Button]
        private void EditorLoadScene() => SceneManager.LoadScene(_scene);
    }
}
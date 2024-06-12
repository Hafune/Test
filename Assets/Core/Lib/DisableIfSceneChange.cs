using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Lib
{
    public class DisableIfSceneChange : MonoBehaviour
    {
        private void Start() => SceneManager.activeSceneChanged += OnSceneChange;

        private void OnDestroy() => SceneManager.activeSceneChanged -= OnSceneChange;

        private void OnSceneChange(Scene scene1, Scene scene2) => gameObject.SetActive(false);
    }
}
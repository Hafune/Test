using System.Collections;
using Lib;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public static class AudioSourceContextMenu
    {
        [MenuItem("CONTEXT/AudioSource/Play")]
        private static void Play(MenuCommand command) => Play((AudioSource)command.context);

        public static void Play(AudioSource source)
        {
            if (!source.clip)
                return;

            var container = new GameObject();
            container.hideFlags = HideFlags.DontSave;
            container.name = "Audio - DontSave";
            EditorCoroutineUtility.StartCoroutine(Play(source, container), container);
        }

        private static IEnumerator Play(AudioSource t, GameObject container)
        {
            if (!t.clip)
                yield break;

            EditorWindow.GetWindow<SceneView>().Focus();
            var source = MyFunctions.CopyComponent(t, container);
            source.PlayOneShot(source.clip);

            while (source && source.isPlaying)
                yield return null;

            Object.DestroyImmediate(container);
        }
    }
}
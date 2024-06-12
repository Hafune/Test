#if UNITY_EDITOR
using Reflex.Injectors;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public static class ExecuteSelectedTask
    {
        [MenuItem("CONTEXT/MonoBehaviour/Execute Task", false, 12)]
        private static void Context(MenuCommand command)
        {
            if (command.context is IMyTask task)
                Run(task);
        }

        [MenuItem("Auto/Execute Selected Task")]
        private static void Auto()
        {
            if (!Selection.activeGameObject)
                return;
            
            if (!Selection.activeGameObject.TryGetComponent<IMyTask>(out var task))
                return;

            Run(task);
        }

        private static void Run(IMyTask task)
        {
            var payload = Payload.GetPooled();
            task.Begin(EditorContextAccess.context, payload, _ => payload.Dispose());
        }
    }
}
#endif
using System.Linq;
using Lib;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public static class MonoBehaviourContext
    {
        [MenuItem("CONTEXT/MonoBehaviour/Format name", false, 11)]
        private static void FormatName(MenuCommand command)
        {
            var value = (MonoBehaviour)command.context;

            if (PrefabUtility.IsPartOfPrefabAsset(value))
                return;

            value.name = value.GetType()
                .Name
                .Replace("Task", "")
                .FormatAddCharBeforeCapitalLetters(false, '_');

            EditorUtility.SetDirty(value);
        }
        
        [MenuItem("CONTEXT/MonoBehaviour/Sort scripts", false, 10)]
        private static void SortScripts(MenuCommand command)
        {
            var owner = (MonoBehaviour)command.context;

            var allComponents = owner.GetComponents<MonoBehaviour>().ToList();
            int selfIndex = allComponents.IndexOf(owner);
            var components = allComponents.GetRange(selfIndex + 1, allComponents.Count - selfIndex - 1);

            var sortedComponents = components.Where(component => component != owner)
                .OrderBy(i => i.ToString()).ToList();

            for (int i = 0, iMax = sortedComponents.Count; i < iMax; i++)
            {
                var component = sortedComponents[i];
                var neededIndex = i + selfIndex + 1;

                while (true)
                {
                    var currentComponents = owner.GetComponents<MonoBehaviour>().ToList();
                    var index = currentComponents.IndexOf(component);

                    if (neededIndex == index)
                        break;

                    bool moveCompleted = true;

                    if (neededIndex < index)
                        moveCompleted = UnityEditorInternal.ComponentUtility.MoveComponentUp(component);
                    if (neededIndex > index)
                        moveCompleted = UnityEditorInternal.ComponentUtility.MoveComponentDown(component);

                    if (moveCompleted)
                        continue;

                    Debug.Log("Не удалось переместить скрипт");
                    return;
                }
            }
        }
    }
}
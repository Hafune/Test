using System.Linq;
using UnityEngine;

namespace Core.Lib
{
    [DisallowMultipleComponent]
    public class SortScriptAfterSelf : MonoBehaviour
    {
        [SerializeField] private bool _resort;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_resort)
                return;

            _resort = false;

            var allComponents = GetComponents<MonoBehaviour>().ToList();
            int selfIndex = allComponents.IndexOf(this);
            var components = allComponents.GetRange(selfIndex + 1, allComponents.Count - selfIndex - 1);

            var sortedComponents = components.Where(component => component != this)
                .OrderBy(i => i.ToString()).ToList();

            for (int i = 0, iMax = sortedComponents.Count; i < iMax; i++)
            {
                var component = sortedComponents[i];
                var neededIndex = i + selfIndex + 1;

                while (true)
                {
                    var currentComponents = GetComponents<MonoBehaviour>().ToList();
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
                    break;
                }
            }
        }
#endif
        private void Awake() => Destroy(this);
    }
}
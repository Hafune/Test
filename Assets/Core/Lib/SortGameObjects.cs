using System.Linq;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class SortGameObjects : MonoBehaviour
    {
        [ContextMenu("Sort objects by name")]
        private void Sort()
        {
            int index = 0;
            foreach (var go in gameObject.transform.GetSelfChildrenTransforms().Select(i => i.gameObject).OrderBy(i => i.name))
                go.transform.SetSiblingIndex(index++);
        }
    }
}
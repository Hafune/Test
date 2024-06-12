#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Core
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Config/" + nameof(TransparentShaderQueue))]
    public class TransparentShaderQueue : ScriptableObject
    {
        private static TransparentShaderQueue Instance;

        [SerializeField] private List<Material> _list;
        [SerializeField] private List<Material> _resetList;

        private void OnValidate()
        {
            if (Instance is not null && Instance != this)
                throw new Exception(nameof(TransparentShaderQueue) + " Дублика");

            Instance = this;

            Refresh();
        }

        private void Refresh()
        {
            var set = new HashSet<Material>();
            _list.RemoveAll(i =>
                i is null ||
                !set.Add(i) ||
                i.renderQueue <= (int)RenderQueue.GeometryLast ||
                !AssetDatabase.IsMainAsset(i));

            for (int i = 0, iMax = _list.Count; i < iMax; i++)
                if (_list[i].renderQueue != (int)RenderQueue.Transparent -50 + i)
                    _list[i].renderQueue = (int)RenderQueue.Transparent -50 + i;

            for (int i = 0, iMax = _resetList.Count; i < iMax; i++)
                _resetList[i].renderQueue = (int)RenderQueue.Transparent;
        }

        [MenuItem("CONTEXT/Material/Add shader to queue")]
        static void Material(MenuCommand command)
        {
            var material = (Material)command.context;

            if (material.renderQueue <= (int)RenderQueue.GeometryLast)
                return;

            Instance._list.Add(material);
            Instance.Refresh();
        }

        [MenuItem("CONTEXT/MeshRenderer/Add shader to queue")]
        static void MeshRenderer(MenuCommand command)
        {
            var meshRenderer = (MeshRenderer)command.context;
            var material = meshRenderer.sharedMaterial;

            if (material.renderQueue <= (int)RenderQueue.GeometryLast)
                return;

            Instance._list.Add(material);
            Instance.Refresh();
        }
    }
}
#endif
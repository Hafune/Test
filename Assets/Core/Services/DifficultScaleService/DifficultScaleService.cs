using System;
using System.Collections.Generic;
using System.Linq;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Services
{
    public class DifficultScaleService : MonoBehaviour
    {
        [SerializeField] private ScaleTemplate[] _templates;
        private Glossary<MyList<ScaleTemplate>> _templateGroups = new();
        private float _sceneScale;

        public IEnumerable<IEcsSystem> InitializeAndBuildSystems() => _templates
            .Select(i =>
            {
                if (!_templateGroups.TryGetValue((int)i.valueEnum, out var list))
                    _templateGroups.Add((int)i.valueEnum, list = new MyList<ScaleTemplate>());

                list.Add(i);
                return i.valueEnum;
            })
            .Distinct()
            .Select(i => ValueScaleSystemsUtility.BuildScaleSystem(i, id =>
            {
                var scales = _templateGroups.GetValue((int)i);
                var total = 1f;
                for (int a = 0, aMax = scales.Count; a < aMax; a++)
                    if (scales.Items[a].TryGet(id, (int)_sceneScale, out float scale))
                        total *= scale;

                return total;
            }));

        public void SetSceneScale(float scale) => _sceneScale = Math.Max(scale, 1);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Lib
{
    [RequireComponent(typeof(MeshRenderer))]
    public class RandomMaterialOnStart : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private List<Material> _materials;
        [SerializeField, Range(0, 1)] private List<float> _weight;
        [SerializeField, HideInInspector] private float _totalWeight;

        private void OnValidate()
        {
            _weight ??= new List<float>();
            _materials ??= new List<Material>();

            while (_weight.Count < _materials.Count)
                _weight.Add(.5f);

            while (_weight.Count > _materials.Count)
                _weight.RemoveAt(_weight.Count - 1);

            _totalWeight = _weight.Sum();
            _meshRenderer = _meshRenderer ? _meshRenderer : GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            var material = GetRandomDecoration();

            if (material == null)
                return;

            _meshRenderer.material = material;
        }

        private Material GetRandomDecoration()
        {
            float value = Random.Range(0, _totalWeight);
            float chance = 0;
            for (int i = 0, c = _materials.Count; i < c; i++)
                if ((chance += _weight[i]) > value)
                    return _materials[i];

            throw new Exception("Не из чего выбирать Оо ?");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Lib
{
    public class SpawnRandomDecoration : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _prefabs;
        [SerializeField, Range(0, 1)] private List<float> _weight;
        [SerializeField, HideInInspector] private float _totalWeight;

        private void OnValidate()
        {
            _weight ??= new List<float>();
            _prefabs ??= new List<GameObject>();

            while (_weight.Count < _prefabs.Count)
                _weight.Add(.5f);

            while (_weight.Count > _prefabs.Count)
                _weight.RemoveAt(_weight.Count - 1);

            _totalWeight = _weight.Sum();
        }

        public void Spawn()
        {
            var prefab = GetRandomDecoration();

            if (prefab == null)
                return;

            Instantiate(prefab, transform, false);
        }

        private GameObject GetRandomDecoration()
        {
            float value = Random.Range(0, _totalWeight);
            float chance = 0;
            for (int i = 0, c = _prefabs.Count; i < c; i++)
                if ((chance += _weight[i]) > value)
                    return _prefabs[i];

            throw new Exception("Не из чего выбирать Оо ?");
        }
    }
}
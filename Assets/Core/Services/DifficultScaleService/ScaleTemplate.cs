using System;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Systems;
using UnityEngine;
using VInspector;

namespace Core.Services
{
    [CreateAssetMenu(menuName = "Game Config/" + nameof(ScaleTemplate))]
    public class ScaleTemplate : ScriptableObject
    {
        [SerializeField, TextArea] private string _rawData;
        [SerializeField] private float[] _scales;
        [SerializeField] private float _base = 1f;
        [SerializeField] private EntitiesTable _table;

        [field: SerializeField] public ValueEnum valueEnum { get; private set; }

        [Button]
        private void ApplyData()
        {
            if (string.IsNullOrEmpty(_rawData))
                return;

            var strings = Regex.Replace(_rawData,",",".")
                .Trim()
                .Split("\n");

            _scales = strings.Select(float.Parse).ToArray();
        }

        public bool TryGet(int id, int level, out float total)
        {
            total = default;
            level -= 1;

            if (!_table.Contains(id))
                return false;

            int index = Math.Min(level, _scales.Length - 1);
            total = _base;
            total *= index < 0 ? 1 : _scales[index];

#if UNITY_EDITOR
            if (index != level)
                Debug.LogWarning($"скейл {level} больше {index}", this);
#endif

            return true;
        }
    }
}
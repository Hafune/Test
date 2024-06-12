using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Core.Lib
{
    [Obsolete("Найти замену рефлексии в рантайме")]
    public class RestoreStateOnDisable : MonoBehaviour
    {
        [SerializeField] private Component _component;
        private Dictionary<FieldInfo, object> _fieldValues = new();
        private Dictionary<PropertyInfo, object> _propValues = new();

        private void Awake()
        {
            var type = _component.GetType();
            var flags = BindingFlags.Public | BindingFlags.Instance;

            var fields = type.GetFields(flags);

            for (int i = 0; i < fields.Length; ++i)
                if (!fields[i].IsStatic)
                    _fieldValues.Add(fields[i], fields[i].GetValue(_component));

            var props = type.GetProperties(flags);

            for (int i = 0; i < props.Length; ++i)
                if (props[i].CanRead && props[i].CanWrite)
                    _propValues.Add(props[i], props[i].GetValue(_component));
        }

        private void OnDisable()
        {
            foreach (var pair in _fieldValues)
            {
                var currentValue = pair.Key.GetValue(_component);
                var savedValue = pair.Value;

                if (currentValue is null && savedValue is null)
                    continue;

                if (currentValue is null || !currentValue.Equals(savedValue))
                    pair.Key.SetValue(_component, savedValue);
            }

            foreach (var pair in _propValues)
            {
                var currentValue = pair.Key.GetValue(_component);
                var savedValue = pair.Value;

                if (currentValue is null && savedValue is null)
                    continue;

                if (currentValue is null || !currentValue.Equals(savedValue))
                    pair.Key.SetValue(_component, savedValue);
            }
        }
    }
}
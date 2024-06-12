using System;
using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Generated;
using Core.Systems;
using JetBrains.Annotations;

namespace Core.EcsCommon.ValueSlotComponents
{
    public static class SlotUtility
    {
        private static Dictionary<Hashtable, ValueData[]> _cache = new();

        public static ValueData[] CalculateValues([CanBeNull] Hashtable values)
        {
            if (values is null)
                return Array.Empty<ValueData>();

            if (_cache.TryGetValue(values, out var v))
                return v;

            var data = new ValueData[values.Count];
            int count = 0;
            foreach (DictionaryEntry entry in values)
                data[count++] = new ValueData
                {
                    valueEnum = (ValueEnum)entry.Key,
                    value = (float)entry.Value
                };

            _cache[values] = data;
            return data;
        }

        public static T Make<T>(ValueData[] values = null, SlotTagEnum[] tags = null) where T : ISlotData, new() =>
            new()
            {
                values = values ?? Array.Empty<ValueData>(),
                tags = tags ?? Array.Empty<SlotTagEnum>(),
            };
    }
}
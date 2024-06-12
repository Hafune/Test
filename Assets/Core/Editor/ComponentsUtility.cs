using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Components;
using Lib;
using UnityEditor;

namespace Core.Editor
{
    public static class ComponentsUtility
    {
        private static string[] ComponentNamesCache;
        private static string[] SlotTagNamesCache;

        private static Dictionary<string, string>
            _stringCache = new();

        private static string[] _valueComponentNames;
        private static string[] _genericValueComponentNames;
        private static string[] _allComponentNames;
        private static string[] _genericNames;
        private static string[] _guids;
        private static string[] _paths;
        private static string _pattern;
        private static List<string> _rawGenericComponents;
        private static string[] _validGenericComponents;
        private static string[] _totalNames;

        public static string[] GetTotalValueComponentNames()
        {
            if (_totalNames is not null)
                return _totalNames;

            _valueComponentNames = GetValueComponentNames();
            _genericValueComponentNames = GetGenericValueComponentNames();
            _totalNames = _valueComponentNames.Concat(_genericValueComponentNames).ToArray();
            return _totalNames;
        }

        public static string[] GetValueComponentNames() => ComponentNamesCache ??= AppDomain.CurrentDomain
            .GetAssignableTypes<IValue>()
            .Select(i => i.Name)
            .OrderBy(i => i)
            .ToArray();

        public static string[] GetSlotTagNames() => SlotTagNamesCache ??= AppDomain.CurrentDomain
            .GetAssignableTypes<ISlotTag>()
            .Select(i => i.Name)
            .OrderBy(i => i)
            .ToArray();

        public static string[] GetGenericValueComponentNames()
        {
            _genericNames ??= AppDomain.CurrentDomain
                .GetAssignableTypes<IValue>(true)
                .Select(i => i.Name.Remove(i.Name.IndexOf('`')))
                .OrderBy(i => i)
                .ToArray();

            _guids = AssetDatabase.FindAssets($"t:Script",
                new[]
                {
                    "Assets/Core/Editor/GeneratedLinks",
                    "Assets/Core/EcsCommon/Providers/ValueProviders"
                });

            if (_guids.Length == 0)
                throw new Exception(
                    "GenInitValuesFromBaseValuesSystem - Количество файлов 0, вероятно указан неверный путь к файлам!");

            _paths = _guids
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid).Replace("Assets", ""))
                .ToArray();

            _pattern ??= string.Join('|', _genericNames);
            _rawGenericComponents ??= new List<string>(_genericNames.Length);
            foreach (var p in _paths)
            foreach (var m in Regex.Matches(TemplateUtility.ReadFile(p),
                         $"({_pattern})<([^<>]|<(?<DEPTH>)|>(?<-DEPTH>))*(?(DEPTH)(?!))>", RegexOptions.Multiline))
                _rawGenericComponents.Add(m.ToString());

            _validGenericComponents ??= FilterGenericNamesByAllComponents(_rawGenericComponents);

            return _validGenericComponents;
        }

        public static string[] FilterGenericNamesByAllComponents(
            IEnumerable<string> rawGenericNames)
        {
            var totalCleanComponentNames = GetAllComponentNames();
            var data = rawGenericNames
                .Select(i => Regex.Replace(i, @"\s+", string.Empty))
                .Distinct()
                .Where(i => Regex
                    .Replace(i, "<|>", ",")
                    .Split(",")
                    .Where(s => s.Trim() != string.Empty)
                    .ToArray()
                    [1..]
                    .Where(i1 => i1.Trim() != string.Empty)
                    .All(totalCleanComponentNames.Contains))
                .OrderBy(i => i)
                .ToArray();

            return data;
        }

        public static string ToFieldName(string value)
        {
            if (_stringCache.TryGetValue(value, out var v))
                return v;

            var s = Regex.Replace(value, "\\W", "");
            s = s.Replace(TypeName(typeof(BaseValueComponent<>)), "Base");
            s = s.Replace(TypeName(typeof(EventActionStart<>)), "EventStart");
            s = s.Replace(TypeName(typeof(EventActionStart<>)), "EventStart");
            s = s.Replace(TypeName(typeof(EventRefreshSlot<>)), "EventRefresh");
            s = s.Replace(TypeName(typeof(EventValueUpdated<>)), "EventUpdated");
            s = s.Replace(TypeName(typeof(EventStartRecalculateValue<>)), "EventStartRecalculate");
            s = s.Replace(TypeName(typeof(RecoverySpeedValueComponent<>)), "RecoverySpeed");
            s = s.Replace(TypeName(typeof(RecoveryActionValueComponent<>)), "ReloadAction");
            s = s.Replace("Component", "");
            s = s.Replace("Tag", "");

            _stringCache.Add(value, s);
            return s;
        }
        
        public static string TypeName(Type t)
        {
            var eName = t.Name;
            return eName.Remove(eName.IndexOf('`'));
        }

        public static string[] GetAllComponentNames()
        {
            if (_allComponentNames is not null)
                return _allComponentNames;

            var totalTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes());

            var namespaceNames = totalTypes
                .Where(type =>
                    type.Namespace == "Core.Components" &&
                    !type.IsClass && !type.IsInterface &&
                    !type.IsByRefLike)
                .ToArray();

            var baseGenericNames = namespaceNames
                .Where(i => i.Name.Contains('`'))
                .Select(i => i.Name.Remove(i.Name.IndexOf('`')))
                .ToArray();

            var nonGenericNames = namespaceNames
                .Where(i => !i.Name.Contains('`'))
                .Select(i => i.Name)
                .ToArray();

            return nonGenericNames.Concat(baseGenericNames).ToArray();
        }
    }
}
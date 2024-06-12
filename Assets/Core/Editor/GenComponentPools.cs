using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Leopotam.EcsLite;
using UnityEditor;

namespace Core.Editor
{
    public static class GenComponentPools
    {
        public const string ComponentPoolsName = "ComponentPools";

        private static string Template = $@"//Файл генерируется в GenComponentPools
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Generated
{{
    // @formatter:off
    public class {ComponentPoolsName}
    {{
        #FIELDS#
    
        public {ComponentPoolsName}({nameof(EcsWorld)} world)
        {{
            #INIT_FIELDS#
        }}
    }}
}}";

        internal static bool Gen()
        {
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

            var guids = AssetDatabase.FindAssets($"t:Script",
                new[]
                {
                    "Assets/Core/Editor/GeneratedLinks",
                    "Assets/Core/EcsCommon/Providers",
                    "Assets/Core/EcsCommon/Components",
                    "Assets/Core/EcsCommon/Systems",
                    "Assets/Core/_GeneratedFiles",
                });

            if (guids.Length == 0)
                throw new Exception(
                    "GenInitValuesFromBaseValuesSystem - Количество файлов 0, вероятно указан неверный путь к файлам!");

            var paths = guids
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid).Replace("Assets", ""))
                .ToArray();

            var pattern = string.Join('|', baseGenericNames);
            var rawRuntimeGenericNames = new List<string>(baseGenericNames.Length);
            
            foreach (var p in paths)
            foreach (var m in Regex.Matches(TemplateUtility.ReadFile(p),
                         $"({pattern})<([^<>]|<(?<DEPTH>)|>(?<-DEPTH>))*(?(DEPTH)(?!))>", RegexOptions.Multiline))
                rawRuntimeGenericNames.Add(m.ToString());

            var validRuntimeGenericNames =
                ComponentsUtility.FilterGenericNamesByAllComponents(rawRuntimeGenericNames);

            var names = nonGenericNames.Concat(validRuntimeGenericNames).OrderBy(i => i).ToArray();

            var fields = names.Select(name => $"public readonly EcsPool<{name}> {R(name)};").ToArray();
            var init_fields = names.Select(name => $"{R(name)} = world.GetPool<{name}>();").ToArray();

            string content = Template;
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FIELDS#", fields);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#INIT_FIELDS#", init_fields);
            
            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(ComponentPoolsName));
        }

        private static string R(string value) => ComponentsUtility.ToFieldName(value);
    }
}
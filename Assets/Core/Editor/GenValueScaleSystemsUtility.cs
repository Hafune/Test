using System.Collections.Generic;
using System.Linq;

namespace Core.Editor
{
    public static class GenValueScaleSystemsUtility
    {
        private const string TargetName = "ValueScaleSystemsUtility";

        private static string Template = $@"//Файл генерируется в {nameof(GenValueScaleSystemsUtility)}
using System;
using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.Services
{{
    // @formatter:off
    public static class {TargetName}
    {{
        public static IEcsSystem BuildScaleSystem(ValueEnum valueEnum, Func<int, float> getScale) => valueEnum switch
        {{
            #FIELDS#
            _ => throw new ArgumentOutOfRangeException(nameof(valueEnum), valueEnum, null)
        }};
    }}
}}";

        internal static bool Gen()
        {
            var names = ComponentsUtility.GetTotalValueComponentNames();
            string R(string name) => ComponentsUtility.ToFieldName(name);

            var fields = names.Select(name =>
                    $"{GenValueEnum.TargetName}.{R(name)} => new ScaleValueSystem<{name}>(getScale),")
                .OrderBy(i => i);

            return Write(fields);
        }

        public static void RemoveGen() => Write(new[] { "//" });

        private static bool Write(IEnumerable<string> fields)
        {
            string content = Template;
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FIELDS#", fields);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
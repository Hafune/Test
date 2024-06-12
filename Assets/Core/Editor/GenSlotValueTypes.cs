using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Systems;

namespace Core.Editor
{
    public static class GenSlotValueTypes
    {
        private const string TargetName = "SlotValueTypes";

        private static string Template = $@"//Файл генерируется в {nameof(GenSlotValueTypes)}
using System;
using Core.Components;
using Core.Systems;

namespace Core.Generated
{{
    public static class {TargetName}
    {{
        // @formatter:off
        public static Type SlotValueType<T>({GenValueEnum.TargetName} @enum)
            where T : struct, {nameof(ISlotData)} => @enum switch
        {{
            #VALUES#
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        }};

        public static Type SlotTagType({GenSlotTagEnum.TargetName} @enum) => @enum switch
        {{
            #TAGS#
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        }};
        // @formatter:on
    }}
}}
";
        internal static bool Gen()
        {
            var baseComponentName = typeof(SlotValueComponent<,>).Name.Replace("`1", "").Replace("`2", "");
            var valueNames = ComponentsUtility.GetTotalValueComponentNames();

            string R(string value) => ComponentsUtility.ToFieldName(value);

            var values = valueNames
                .Select(name => $"{GenValueEnum.TargetName}.{R(name)} => typeof({baseComponentName}<T, {name}>),")
                .OrderBy(i => i);

            var tagNames = ComponentsUtility.GetSlotTagNames();

            var tags = tagNames.Select(name => $"{GenSlotTagEnum.TargetName}.{R(name)} => typeof({name}),")
                .OrderBy(i => i);

            return Write(
                values,
                tags
            );
        }

        public static void RemoveGen() => Write(
            new[] { "//" },
            new[] { "//" }
        );

        private static bool Write(
            IEnumerable<string> values,
            IEnumerable<string> tags
        )
        {
            string content = TemplateUtility.ReplaceTagWithIndentedMultiline(Template, "#VALUES#", values);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#TAGS#", tags);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
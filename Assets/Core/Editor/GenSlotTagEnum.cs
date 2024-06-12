using System.Collections.Generic;
using System.Linq;
using Core.Systems;
using UnityEngine;

namespace Core.Editor
{
    public static class GenSlotTagEnum
    {
        public const string TargetName = "SlotTagEnum";

        private static string Template = $@"//Файл генерируется в {nameof(GenSlotTagEnum)}
namespace Core.Generated
{{
    public enum {TargetName}
    {{
          #FIELDS#
    }}
}}";

        internal static bool Gen()
        {
            Dictionary<string, int> keyValues = new();
            var names = ComponentsUtility.GetSlotTagNames();
            
            foreach (var name in names)
                keyValues[ComponentsUtility.ToFieldName(name)] = Animator.StringToHash(name);

            var fields = keyValues.Select(pair => $"{pair.Key} = {pair.Value},").OrderBy(i => i);

            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(Template, "#FIELDS#", fields);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Editor
{
    public static class GenValueEnum
    {
        public const string TargetName = "ValueEnum";
        private static string Template = $@"//Файл генерируется в {nameof(GenValueEnum)}
namespace Core.Systems
{{
    public enum {TargetName}
    {{
        #FIELDS#
    }}
}}";
        internal static bool Gen()
        {
            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            var names = ComponentsUtility.GetTotalValueComponentNames();
            
            foreach (var name in names)
            {
                keyValues[ComponentsUtility.ToFieldName(name)] = Animator.StringToHash(name);
            }

            var fields = keyValues.Select(pair => $"{pair.Key} = {pair.Value},").OrderBy(i => i);

            string content = Template;
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FIELDS#", fields);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Lib;
using UnityEngine;

namespace Core.Editor
{
    public static class GenActionEnum
    {
        public const string ActionEnum = "ActionEnum";
        private static string Template = $@"//Файл генерируется в {nameof(GenActionEnum)}
namespace Core.Generated
{{
    public enum {ActionEnum}
    {{
        #FIELDS#
    }}
}}";
        public static bool Gen()
        {
            Dictionary<string, int> keyValues = new();
            var implementingTypes = AppDomain.CurrentDomain.GetAssignableTypes<IActionComponent>();

            foreach (var implementingType in implementingTypes)
                keyValues[implementingType.Name] = Animator.StringToHash(implementingType.Name);

            var fields = keyValues.Select(pair => $"{pair.Key} = {pair.Value},").OrderBy(i => i);

            string content = Template;
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FIELDS#", fields);
            return TemplateUtility.WriteScriptIfDifferent(content, $"{GenLauncher.DIR}{ActionEnum}.cs");
        }
    }
}
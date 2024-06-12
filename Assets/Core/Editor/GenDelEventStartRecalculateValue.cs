using System.Linq;
using Core.Systems;
using UnityEngine;

namespace Core.Editor
{
    public static class GenDelEventStartRecalculateValue
    {
        private const string TargetName = "DelEventStartRecalculateValue";
        
        private static string Template = $@"//Файл генерируется в {nameof(GenDelEventStartRecalculateValue)}
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{{
    // @formatter:off
    public class {TargetName} : IEcsRunSystem
    {{
        #FILTERS#
                
        #POOLS#

        public void Run(IEcsSystems systems)
        {{
            #ITERATORS#
        }}
    }}
}}";
        
        internal static bool Gen()
        {
            var totalNames = ComponentsUtility.GetTotalValueComponentNames();

            string R(string value) => ComponentsUtility.ToFieldName(value);

            var filters = totalNames.Select(name =>
                $"private readonly EcsFilterInject<Inc<EventStartRecalculateValue<{name}>>> _{R(name)}Filter;");

            var pools = totalNames.Select(name =>
                $"private readonly EcsPoolInject<EventStartRecalculateValue<{name}>> _{R(name)}Pool;");

            var iterators = totalNames.Select(name =>
                $"foreach (var i in _{R(name)}Filter.Value) _{R(name)}Pool.Value.Del(i);");

            string content = Template;
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FILTERS#", filters);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#POOLS#", pools);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#ITERATORS#", iterators);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }

        public static void RemoveGen()
        {
            string content = Template;
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FILTERS#", new[] { "//" });
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#POOLS#", new[] { "//" });
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#ITERATORS#", new[] { "//" });

            TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}

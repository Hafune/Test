using System.Linq;
using Core.Systems;
using UnityEngine;

namespace Core.Editor
{
    public static class GenInitValuesFromBaseValuesSystem
    {
        private const string TargetName = "InitValuesFromBaseValuesSystem";

        private static string Template = $@"//Файл генерируется в {nameof(GenInitValuesFromBaseValuesSystem)}
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{{
    // @formatter:off
    public class {TargetName} : IEcsRunSystem
    {{
        private readonly EcsFilterInject<Inc<EventInit>> _hasInitFilter;
        
        #FILTERS#

        #POOLS#

        public void Run(IEcsSystems systems)
        {{
            if (_hasInitFilter.Value.GetEntitiesCount() == 0)
                return;
            
            #ITERATORS#
        }}
    }}
}}";

        internal static bool Gen()
        {
            var totalNames = ComponentsUtility.GetTotalValueComponentNames();

            string R(string value) => ComponentsUtility.ToFieldName(value);

            var filters = totalNames.Select(name =>
                $"private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<{name}>>> _base{R(name)}Filter;");

            var pools = totalNames.SelectMany(name => new[]
            {
                $"private readonly EcsPoolInject<BaseValueComponent<{name}>> _base{R(name)}Pool;",
                $"private readonly EcsPoolInject<{name}> _{R(name)}Pool;",
            });

            var iterators = totalNames.Select(name =>
                $"foreach (var i in _base{R(name)}Filter.Value) _{R(name)}Pool.Value.Add(i).value = _base{R(name)}Pool.Value.Get(i).baseValue;");

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

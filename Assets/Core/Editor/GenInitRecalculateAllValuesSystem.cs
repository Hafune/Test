using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Systems;

namespace Core.Editor
{

    public static class GenInitRecalculateAllValuesSystem
    {
        private const string TargetName = "InitRecalculateAllValuesSystem";

        private static string Template = $@"//Файл генерируется в {nameof(GenInitRecalculateAllValuesSystem)}
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{{
    // @formatter:off
    public class {TargetName} : IEcsRunSystem
    {{
        #FILTERS#
        
        private readonly ComponentPools _pools;

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
            var eventStart = ComponentsUtility.TypeName(typeof(EventStartRecalculateValue<>));
            
            var filters = totalNames.Select(name =>
                $"private readonly EcsFilterInject<Inc<{nameof(EventInit)}, {name}>, Exc<{eventStart}<{name}>>> _{R(name)}Filter;");

            var iterators = totalNames.Select(name =>
                $"foreach (var i in _{R(name)}Filter.Value) _pools.{R(eventStart + name)}.Add(i);");

            return Write(filters, iterators);
        }

        public static void RemoveGen() => Write(
            new[] { "//" },
            new[] { "//" }
        );

        private static bool Write(
            IEnumerable<string> filters,
            IEnumerable<string> iterators)
        {
            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(Template, "#FILTERS#", filters);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#ITERATORS#", iterators);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
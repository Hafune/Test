using System.Collections.Generic;
using System.Linq;
using Core.Systems;

namespace Core.Editor
{
    public static class GenEventRefreshValueBySlotSystem
    {
        private const string TargetName = "EventRefreshValueBySlotSystem";

        private static string Template = $@"//Файл генерируется в {nameof(GenEventRefreshValueBySlotSystem)}
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using static Core.Components.SlotFilterAndPools;

namespace Core.Generated
{{
    // @formatter:off
    public class {TargetName} : IEcsRunSystem
    {{
        #FIELDS#
        
        public {TargetName}(EcsWorld world)
        {{
            #CTOR#
        }}

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

            var fields = totalNames.SelectMany(name => new[]
            {
                $"private readonly EcsFilterInject<Inc<EventValueUpdated<{name}>>> {R(name)}Filter;",
                $"private readonly EcsPoolInject<{name}> {R(name)}Pool;",
                $"private readonly RefreshValueBySlotWrapper<{name}> {R(name)}Wrapper;"
            });

            var ctor = totalNames.Select(name => $"{R(name)}Wrapper = MakeSlotFiltersAndPools<{name}>(world);");

            //Подумать о вызове без алокаций
            var iterators = totalNames.Select(name =>
                $"if ({R(name)}Filter.Value.GetEntitiesCount() != 0) {R(name)}Wrapper.AddSlotValues({R(name)}Pool.Value);");

            return Write(
                fields,
                ctor,
                iterators
            );
        }

        public static void RemoveGen() => Write(
            new[] { "//" },
            new[] { "//" },
            new[] { "//" }
        );

        private static bool Write(
            IEnumerable<string> fields,
            IEnumerable<string> ctor,
            IEnumerable<string> iterators
        )
        {
            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(Template, "#FIELDS#", fields);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#CTOR#", ctor);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#ITERATORS#", iterators);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
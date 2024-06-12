using System.Linq;
using Core.Systems;
using UnityEngine;

namespace Core.Editor
{
    public static class GenEventStartRecalculateValueSystem
    {
        private const string TargetName = "EventStartRecalculateValueSystem";

        private static string Template = $@"//Файл генерируется в {nameof(GenEventStartRecalculateValueSystem)}
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

            var filters = totalNames.SelectMany(name => new[]
            {
                $"private readonly EcsFilterInject<Inc<EventValueUpdated<{name}>>> _eventUpdated{R(name)}Filter;",
                $"private readonly EcsFilterInject<Inc<{name}, EventStartRecalculateValue<{name}>, BaseValueComponent<{name}>>> _base{R(name)}Filter;",
            });

            var pools = totalNames.SelectMany(name => new[]
            {
                $"private readonly EcsPoolInject<{name}> _{R(name)}Pool;",
                $"private readonly EcsPoolInject<BaseValueComponent<{name}>> _base{R(name)}Pool;",
                $"private readonly EcsPoolInject<EventValueUpdated<{name}>> _eventUpdated{R(name)}Pool;",
            });

            var iterators = totalNames.SelectMany(name => new[]
            {
                $"foreach (var i in _eventUpdated{R(name)}Filter.Value) _eventUpdated{R(name)}Pool.Value.Del(i);",
                $"foreach (var i in _base{R(name)}Filter.Value) {{ _{R(name)}Pool.Value.Get(i).value = _base{R(name)}Pool.Value.Get(i).baseValue; _eventUpdated{R(name)}Pool.Value.Add(i); }}",
            });

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

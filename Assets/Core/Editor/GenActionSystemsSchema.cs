using System;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Components;
using Core.Generated;
using Lib;
using NaturalSort.Extension;

namespace Core.Editor
{
    public static class GenActionSystemsSchema
    {
        private const string ActionSystemsSchemaName = "ActionSystemsSchema";

        private static string ActionSystemsSchemaTemplate = $@"//Файл генерируется в GenActionSystemsSchema
using System;
using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.Generated
{{
    // @formatter:off
    public class {ActionSystemsSchemaName}
    {{
        #POOLS#
        
        public {ActionSystemsSchemaName}(EcsWorld world)
        {{
            #CONSTRUCTOR_POOLS#
        }}
        
        public void AddEventActionStart(ActionEnum actionEnum, int entity)
        {{
            switch (actionEnum)
            {{
                #FIELDS#
            }}
        }}

        public static ActionEnum Get<T>(T c) where T : IActionComponent =>  
            c switch 
            {{
                #ENUM#
                _ => throw new ArgumentOutOfRangeException()
            }};
    }}
    // @formatter:on
}}";

        private const string PlaybackAndRecordingActionsSystemName = "PlaybackAndRecordingActionsSystem";

        private static string PlaybackAndRecordingActionsSystemTemplate = $@"//Файл генерируется в GenActionSystemsSchema
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{{
    // @formatter:off
    public class {PlaybackAndRecordingActionsSystemName} : IEcsRunSystem
    {{
        private readonly EcsFilterInject<Inc<WriteCommandsTag>> _writeCommandsFilter;
        #FILTERS#

        public void Run(IEcsSystems systems)
        {{
            if (_writeCommandsFilter.Value.GetEntitiesCount() == 0)
                return;
                
            #LOGIC#
        }}
    }}
    // @formatter:on
}}";
        internal static bool Gen()
        {
            var componentNames = AppDomain.CurrentDomain
                .GetAssignableTypes<IActionComponent>()
                .Select(i => i.Name)
                .OrderBy(i => i.Replace("Component", ""), StringComparison.OrdinalIgnoreCase.WithNaturalSort())
                .ToArray();

            string[] filteredNames = FilterNames(componentNames);

            return UpdateSchemaFile(filteredNames) |
                   UpdateSystemFile(filteredNames); //|
            // UpdateLinksFile(filteredNames);
        }

        public static void RemoveGen()
        {
        }

        private static string[] FilterNames(string[] array)
        {
            var newArray = array.ToArray();

            for (int i = 0; i < newArray.Length; i++)
            for (int a = i + 1; a < newArray.Length; a++)
            {
                var s1 = newArray[a];
                var s2 = Regex.Replace(newArray[i], @"(\d+Component)|(Component)", "");

                if (!s1.Contains(s2))
                    continue;

                newArray[a] = newArray[i];
                break;
            }

            return newArray;
        }

        private static bool UpdateSchemaFile(string[] filteredNames)
        {
            var pools = new string[filteredNames.Length];
            var constructorPools = new string[filteredNames.Length];
            var fields = new string[filteredNames.Length];
            var enu = new string[filteredNames.Length];

            var baseEventName = typeof(EventActionStart<>).Name.Replace("`1", "");

            for (int i = 0, iMax = filteredNames.Length; i < iMax; i++)
            {
                var componentName = filteredNames[i];
                pools[i] = $"private readonly EcsPool<{baseEventName}<{componentName}>> {componentName}Pool;";
                constructorPools[i] = $"{componentName}Pool = world.GetPool<{baseEventName}<{componentName}>>();";
                fields[i] = $"case {GenActionEnum.ActionEnum}.{componentName}: {componentName}Pool.Add(entity); break;";
                enu[i] = $"{componentName} => ActionEnum.{componentName},";
            }

            string content = ActionSystemsSchemaTemplate;

            // @formatter:off
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#POOLS#", pools);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#CONSTRUCTOR_POOLS#", constructorPools);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FIELDS#", fields);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#ENUM#", enu);
            // @formatter:on

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(ActionSystemsSchemaName));
        }

        private static bool UpdateSystemFile(string[] filteredNames)
        {
            var filters = new string[filteredNames.Length];
            var logics = new string[filteredNames.Length];

            for (int i = 0, iMax = filteredNames.Length; i < iMax; i++)
            {
                var componentName = filteredNames[i];
                filters[i] =
                    $"private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<{componentName}>>> {componentName}Filter;";
                logics[i] = $"foreach (var i in {componentName}Filter.Value) {{}}";
            }

            string content = PlaybackAndRecordingActionsSystemTemplate;

            // @formatter:off
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FILTERS#", filters);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#LOGIC#", logics);
            // @formatter:on

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(PlaybackAndRecordingActionsSystemName));
        }

        private const string EventActionCompleteLinksName = "_EventActionCompleteLinks";

        private static string LintTemplate = $@"//Файл генерируется в {nameof(GenActionSystemsSchema)}
using Core.Components;

namespace Core.Editor
{{
    public class {EventActionCompleteLinksName}
    {{
        private readonly string[] updated =
        {{
            #UPDATED#
        }};
    }}
}}";

        // private static bool UpdateLinksFile(string[] names)
        // {
        //     var updatedName = ComponentsUtility.TypeName(typeof(EventValueUpdated<>));
        //     var reloadName = ComponentsUtility.TypeName(typeof(ReloadActionValueComponent<>));
        //
        //     var updated = new[] { $"nameof({updatedName}<{reloadName}<{nameof(ActionDashComponent)}>>)," };
        //     
        //     var content = TemplateUtility.ReplaceTagWithIndentedMultiline(LintTemplate, "#UPDATED#", updated);
        //     
        //     return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakeEditorPath(EventActionCompleteLinksName));
        //     return false;
        // }
    }
}

/*
        public static ActionEnum Get<T>(T c) where T : IActionComponent =>  
            c switch 
            {
                ActionIdleComponent => ActionEnum.ActionIdleSystem,
                _ => throw new ArgumentOutOfRangeException()
            };
*/
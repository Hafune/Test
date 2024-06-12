using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Leopotam.EcsLite;
using Lib;

namespace Core.Editor
{
    public static class GenSlotFilterAndPools
    {
        private const string TargetName = "SlotFilterAndPools";

        private static string Template = $@"//Файл генерируется в {nameof(GenSlotFilterAndPools)}
// @formatter:off
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.Components
{{
    public static class {TargetName}
    {{
        public static RefreshValueBySlotWrapper<T> MakeSlotFiltersAndPools<T>({nameof(EcsWorld)} world)
            where T : struct, {nameof(IValue)} => new(world);

        private static EcsFilter MakeFilter<S, T>({nameof(EcsWorld)} world)
            where S : struct, {nameof(ISlotData)}
            where T : struct, {nameof(IValue)} => world.Filter<T>()
            .Inc<EventValueUpdated<T>>().Inc<SlotValueComponent<S, T>>().End();

        private static EcsPool<SlotValueComponent<S, T>> MakePool<S, T>({nameof(EcsWorld)} world)
            where S : struct, {nameof(ISlotData)}
            where T : struct, {nameof(IValue)} =>
            world.GetPool<SlotValueComponent<S, T>>();

        public class RefreshValueBySlotWrapper<T> where T : struct, {nameof(IValue)}
        {{
            #FILTERS#

            #POOLS#

            public RefreshValueBySlotWrapper({nameof(EcsWorld)} world)
            {{
                #CTOR_FILTERS#

                #CTOR_POOLS#
            }}

            public void AddSlotValues(EcsPool<T> targetPool)
            {{
                #VALUES#
            }}
        }}
    }}
}}";

        private const string _TargetName = nameof(_EventRefreshSlotLinks);

        private static string _Template = $@"//Файл генерируется в {nameof(GenSlotFilterAndPools)}
using Core.Components;

namespace Core.Editor
{{
    public class {_TargetName}
    {{
        private readonly string[] links =
        {{
            #FIELDS#
        }};
    }}
}}";

        internal static bool Gen()
        {
            var names = AppDomain.CurrentDomain
                .GetAssignableTypes<ISlotData>()
                .Select(i => i.Name)
                .OrderBy(i => i);

            string R(string value) => ComponentsUtility.ToFieldName(value);
            var filters = names.Select(name => $"private readonly EcsFilter {R(name)}Filter;");
            var pools = names.Select(name => $"private readonly EcsPool<SlotValueComponent<{name}, T>> {R(name)}Pool;");

            var ctorFilters = names.Select(name => $"{R(name)}Filter = MakeFilter<{name}, T>(world);");
            var ctorPools = names.Select(name => $"{R(name)}Pool = MakePool<{name}, T>(world);");

            var values = names.Select(name =>
                $"foreach (var entity in {R(name)}Filter) targetPool.Get(entity).value += {R(name)}Pool.Get(entity).value;"
            );

            var eventName = typeof(EventRefreshSlot<>).Name.Replace("`1", "");
            var links = names.Select(name => $"nameof({eventName}<{name}>),");

            return Write(filters, pools, ctorFilters, ctorPools, values) | WriteLinks(links);
        }

        public static void RemoveGen()
        {
            Write(
                new[] { "//" },
                new[] { "//" },
                new[] { "//" },
                new[] { "//" },
                new[] { "//" }
            );

            WriteLinks(
                new[] { "//" }
            );
        }

        private static bool Write(
            IEnumerable<string> filters,
            IEnumerable<string> pools,
            IEnumerable<string> ctorFilters,
            IEnumerable<string> ctorPools,
            IEnumerable<string> values)
        {
            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(Template, "#FILTERS#", filters);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#POOLS#", pools);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#CTOR_FILTERS#", ctorFilters);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#CTOR_POOLS#", ctorPools);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#VALUES#", values);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }

        private static bool WriteLinks(IEnumerable<string> links)
        {
            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(_Template, "#FIELDS#", links);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakeEditorPath(_TargetName));
        }
    }
}

/*
public class SlotFilterAndPools<T> where T : struct, IValue
        {
            private readonly EcsFilter AbilitiesSlotFilter;
            
            private readonly EcsPool<SlotValueComponent<AbilitiesSlotComponent, T>> AbilitiesSlotPool;

            public SlotFilterAndPools(EcsWorld world)
            {
                AbilitiesSlotFilter = MakeFilter<AbilitiesSlotComponent, T>(world);
                
                AbilitiesSlotPool = MakePool<AbilitiesSlotComponent, T>(world);
            }

            public void AddSlotValues(EcsPool<T> targetPool)
            {
                foreach (var entity in AbilitiesSlotFilter)
                    targetPool.Get(entity).value += AbilitiesSlotPool.Get(entity).value;
            }
        }
        */
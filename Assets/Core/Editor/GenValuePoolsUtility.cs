using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Systems;

namespace Core.Editor
{
    public static class GenValuePoolsUtility
    {
        private const string TargetName = nameof(ValuePoolsUtility);

        private static string Template = $@"//Файл генерируется в {nameof(GenValuePoolsUtility)}
using System;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;

namespace Core.Components
{{    
    // @formatter:off
    public static class {TargetName}
    {{
        public static void Sum({GenComponentPools.ComponentPoolsName} pools, int entity, {GenValueEnum.TargetName} @enum, float byValue)
        {{
            switch (@enum)
            {{
                #INCREMENT_BY_FLOAT#
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }}
        }}
        
        public static void Sum({GenComponentPools.ComponentPoolsName} pools, int entity, {GenValueEnum.TargetName} @enum, {GenValueEnum.TargetName} byValue)
        {{
            switch (@enum)
            {{
                #INCREMENT_BY_VALUE#
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }}
        }}
        
        public static float GetValue({GenComponentPools.ComponentPoolsName} pools, int entity, {GenValueEnum.TargetName} @enum) => @enum switch
        {{
            #GET#
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        }};

        public static IEcsPool GetStartRecalculatePool({GenComponentPools.ComponentPoolsName} pools, {GenValueEnum.TargetName} @enum) => @enum switch
        {{
            #GET_START_RECALCULATE_POOL#
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        }};
        
        public static void SetValue({GenComponentPools.ComponentPoolsName} pools, int entity, {GenValueEnum.TargetName} @enum, float value)
        {{
            switch (@enum)
            {{
                #SET#
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }}
        }}
    }}
}}";

        internal static bool Gen()
        {
            var names = ComponentsUtility.GetTotalValueComponentNames();
            var eName = typeof(EventValueUpdated<>).Name;
            eName = eName.Remove(eName.IndexOf('`'));

            string R(string value) => ComponentsUtility.ToFieldName(value);

            var byFloat = names.Select(name =>
                $"case {GenValueEnum.TargetName}.{R(name)}: pools.{R(name)}.Get(entity).value += byValue; pools.{R(eName + name)}.AddIfNotExist(entity); break;");

            var byValue = names.Select(name =>
                $"case {GenValueEnum.TargetName}.{R(name)}: pools.{R(name)}.Get(entity).value += GetValue(pools, entity, byValue); pools.{R(eName + name)}.AddIfNotExist(entity); break;");

            var get = names.Select(name =>
                $"{GenValueEnum.TargetName}.{R(name)} => pools.{R(name)}.Get(entity).value,");

            var eventName = ComponentsUtility.TypeName(typeof(EventStartRecalculateValue<>));
            var start = names.Select(name =>
                $"{GenValueEnum.TargetName}.{R(name)} => pools.{R(eventName + name)},");

            var set = names.Select(name =>
                $"case {GenValueEnum.TargetName}.{R(name)}: pools.{R(name)}.Get(entity).value = value; pools.{R(eName + name)}.AddIfNotExist(entity); break;");

            return Write(byFloat, byValue, get, start, set);
        }

        public static void RemoveGen() => Write(
            new[] { "//" },
            new[] { "//" },
            new[] { "//" },
            new[] { "//" },
            new[] { "//" }
        );

        private static bool Write(
            IEnumerable<string> byFloat,
            IEnumerable<string> byValue,
            IEnumerable<string> get,
            IEnumerable<string> start,
            IEnumerable<string> set)
        {
            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(Template, "#INCREMENT_BY_FLOAT#", byFloat);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#INCREMENT_BY_VALUE#", byValue);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#GET#", get);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#GET_START_RECALCULATE_POOL#", start);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#SET#", set);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
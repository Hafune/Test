using System;
using System.Linq;
using Core.Components;
using Core.Systems;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Editor
{
    public static class GenSlotSetupSystemsNode
    {
        private const string TargetName = nameof(SlotSetupSystemsNode);
        private static string Template = $@"//Файл генерируется в {nameof(GenSlotSetupSystemsNode)}
using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Systems
{{
    public class {TargetName}
    {{
        public IEnumerable<IEcsSystem> BuildSystems({nameof(Context)} _context)
        {{
            var world = _context.Resolve<EcsWorld>();
            var systems = new List<IEcsSystem>(16);

            void A<T>() where T : struct, {nameof(ISlotData)} => systems.Add(new {ComponentsUtility.TypeName(typeof(SlotSystem<>))}<T>(world));

            #FIELDS#

            return systems;
        }}
    }}
}}
";
        internal static bool Gen()
        {
            var names = AppDomain.CurrentDomain.GetAssignableTypes<ISlotData>()
                .Select(i => i.Name)
                .OrderBy(i => i);
            var fields = names.Select(name => $"A<{name}>();");

            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(Template, "#FIELDS#", fields);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }

        public static void RemoveGen()
        {
            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(Template, "#FIELDS#", new[] { "//" });

            TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
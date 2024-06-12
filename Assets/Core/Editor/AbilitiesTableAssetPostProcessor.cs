using System.Collections.Generic;
using System.Linq;
using BansheeGz.BGDatabase;
using UnityEditor;

namespace Core.Editor
{
    public class AbilitiesTableAssetPostProcessor : AssetPostprocessor
    {
        private const string FILE_PATH = "/Core/Services/AbilitiesService/";
        private const string BD_PATH = "Assets/Resources/bansheegz_database.bytes";

//         private static readonly string TEMPLATE = $@"//Файл генерируется в {nameof(AbilitiesTableAssetPostProcessor)}
// public partial class {nameof(Abilities)}
// {{
//     #FIELDS#
// }}";
        // private static void GenerateTableAccessorFile(IEnumerable<BGEntity> entities, string fileName)
        // {
        //     var filePath = $"{FILE_PATH}{fileName}.cs";
        //
        //     if (GenerateFileContent(entities, filePath))
        //         AssetDatabase.ImportAsset($"Assets{filePath}");
        // }
        //
        // private static bool GenerateFileContent(IEnumerable<BGEntity> entities, string filePath)
        // {
        //     var fields = entities.Select(i => $"public static {nameof(Abilities)} {i.Name} => GetEntity({i.Index});");
        //     var content = TemplateUtility.ReplaceTagWithIndentedMultiline(TEMPLATE, "#FIELDS#", fields);
        //
        //     return TemplateUtility.WriteScriptIfDifferent(content, filePath);
        // }

        private static void OnPostprocessAllAssets(
            string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!importedAssets.Contains(BD_PATH))
                return;

            // GenerateTableAccessorFile(
            //     Abilities
            //         .FindEntities(_ => true)
            //         .Select(i => i as BGEntity),
            //     nameof(Abilities));
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BansheeGz.BGDatabase;
using BansheeGz.BGDatabase.Editor;
using Core.EcsCommon.ValueComponents;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    public class ItemDatabaseTableAssetPostProcessor : AssetPostprocessor
    {
        private const string TARGET = "ItemDatabaseEnum";
        private const string FILE_PATH = "/Core/_GeneratedFiles/";
        private const string BD_PATH = "Assets/Resources/bansheegz_database.bytes";

        private static string TEMPLATE = $@"//Файл генерируется в {nameof(ItemDatabaseTableAssetPostProcessor)}
namespace Core
{{
    public enum {TARGET}
    {{
        #FIELDS#
    }}
}}";

        // private static void RefreshItemDatabase()
        // {
        //     bool isDirty = false;
        //     
        //     Weapons.ForEachEntity(i =>
        //     {
        //         var (id, dirty) = UpdateItemDatabase(i, ItemSourceEnum.WeaponService);
        //         isDirty = isDirty || dirty;
        //         i.globalId = id;
        //     });
        //
        //     Enhancements.ForEachEntity(i =>
        //     {
        //         var (id, dirty) = UpdateItemDatabase(i, ItemSourceEnum.EnhancementService);
        //         isDirty = isDirty || dirty;
        //         i.globalId = id;
        //     });
        //
        //     if (isDirty)
        //         BGRepoSaver.SaveAndMarkAsSaved();
        //
        //     Dictionary<string, int> keyValues = new();
        //
        //     foreach (var item in ItemDatabase.FindEntities(null))
        //     {
        //         var key = $"{MyEnumUtility<ItemSourceEnum>.Name((int)item.source).Replace("Service", "")}_{item.Name}";
        //
        //         key += "_ID" + item.Index;
        //         keyValues[key] = item.Index;
        //     }
        //
        //     var fields = keyValues.Select(pair => $"{pair.Key} = {pair.Value},").OrderBy(i => i);
        //
        //     string content = TemplateUtility.ReplaceTagWithIndentedMultiline(TEMPLATE, "#FIELDS#", fields);
        //
        //     if (TemplateUtility.WriteScriptIfDifferent(content, $"{FILE_PATH}{TARGET}.cs"))
        //         AssetDatabase.Refresh();
        // }
        //
        // private static (int, bool) UpdateItemDatabase(IDropItem i, ItemSourceEnum source)
        // {
        //     bool isDirty = false;
        //     var item = ItemDatabase.FindEntity(item => item.source == source && item.key == i.Id.ToString());
        //     item ??= ItemDatabase.NewEntity();
        //
        //     if (item.source != source)
        //     {
        //         item.source = source;
        //         isDirty = true;
        //     }
        //
        //     var name = source != ItemSourceEnum.Prefab ? i.name + i.Level : i.name;
        //     if (item.name != name)
        //     {
        //         item.name = name;
        //         isDirty = true;
        //     }
        //
        //     item.key = i.Id.ToString();
        //
        //     return (item.Index, isDirty);
        // }

        private static void OnPostprocessAllAssets(
            string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!importedAssets.Contains(BD_PATH))
                return;

            // RefreshItemDatabase();
        }
    }
}
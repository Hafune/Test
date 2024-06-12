using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Lib
{
    public static class MyLocalizationExtensions
    {
        public static bool HasLocalizedString(this LocalizedStringTable table, in string key) => table.GetTable().GetEntry(key) != null;
        
        public static string GetLocalizedString(this LocalizedStringTable table, in string key) =>
            LocalizationSettings
                .StringDatabase
                .GetLocalizedString(
                    table.TableReference,
                    key);
        
        public static string GetLocalizedString(this LocalizedStringTable table, long key) =>
            LocalizationSettings
                .StringDatabase
                .GetLocalizedString(
                    table.TableReference,
                    key);
    }
}
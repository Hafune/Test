#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Microsoft.Win32;

namespace Core
{
    public class PlayerPrefsWindow : EditorWindow
    {
        private const string PlayerPrefsFilePathTemplate = "Assets/PlayerPrefsData_Slot{0}.json";
        private int selectedSlot = 0;
        private string[] slotLabels = new string[5];

        [MenuItem("Auto/Player Prefs/Open PlayerPrefs Window")]
        private static void ShowWindow() => GetWindow<PlayerPrefsWindow>("PlayerPrefs Manager");

        private void OnEnable() => UpdateSlotLabels();

        private void OnGUI()
        {
            GUILayout.Label("Select Slot", EditorStyles.boldLabel);

            selectedSlot = GUILayout.SelectionGrid(selectedSlot, slotLabels, 1);

            GUILayout.Space(20);

            if (GUILayout.Button("Save PlayerPrefs to File"))
            {
                SavePlayerPrefsToFile(selectedSlot);
                UpdateSlotLabels();
            }

            if (GUILayout.Button("Load PlayerPrefs from File"))
            {
                LoadPlayerPrefsFromFile(selectedSlot);
                UpdateSlotLabels();
            }

            if (GUILayout.Button("Clear Current Prefs"))
                ClearPlayerPrefs();
        }

        private void SavePlayerPrefsToFile(int slot)
        {
            var filePath = string.Format(PlayerPrefsFilePathTemplate, slot + 1);
            EnsureDirectoryExists(filePath);

            var allPrefs = new PlayerPrefsData();
            var keys = GetPlayerPrefsKeys();
            foreach (var key in keys)
            {
                if (PlayerPrefs.HasKey(key))
                {
                    allPrefs.prefs.Add(new PlayerPrefEntry(key, PlayerPrefs.GetString(key)));
                }
            }

            var json = JsonUtility.ToJson(allPrefs, true);
            File.WriteAllText(filePath, json);
            AssetDatabase.Refresh();

            Debug.Log($"PlayerPrefs saved to {filePath}");
        }

        private void LoadPlayerPrefsFromFile(int slot)
        {
            var filePath = string.Format(PlayerPrefsFilePathTemplate, slot + 1);

            if (!File.Exists(filePath))
            {
                Debug.LogError($"File not found: {filePath}");
                return;
            }

            var json = File.ReadAllText(filePath);
            var allPrefs = JsonUtility.FromJson<PlayerPrefsData>(json);

            PlayerPrefs.DeleteAll();
            foreach (var entry in allPrefs.prefs)
            {
                PlayerPrefs.SetString(entry.key, entry.value);
            }

            PlayerPrefs.Save();

            Debug.Log($"PlayerPrefs loaded from {filePath}");
        }

        private void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            Debug.Log("All PlayerPrefs have been cleared.");
        }

        private void UpdateSlotLabels()
        {
            for (int i = 0; i < slotLabels.Length; i++)
            {
                var filePath = string.Format(PlayerPrefsFilePathTemplate, i + 1);
                if (File.Exists(filePath))
                {
                    var lastWriteTime = File.GetLastWriteTime(filePath);
                    slotLabels[i] = $"Slot {i + 1}  {lastWriteTime:yy.MM.dd HH:mm:ss}";
                }
                else
                {
                    slotLabels[i] = $"Slot {i + 1}  empty";
                }
            }
        }

        private static string[] GetPlayerPrefsKeys()
        {
            var PlayerPrefsRegistryPath = @"Software\Unity\UnityEditor\" + Application.companyName + @"\" +
                                          Application.productName;
            using var rootKey = Registry.CurrentUser.OpenSubKey(PlayerPrefsRegistryPath);

            if (rootKey == null)
                return Array.Empty<string>();

            var keys = rootKey.GetValueNames()
                .Select(key => key[..key.LastIndexOf("_h", StringComparison.Ordinal)])
                .ToArray();

            return EncodeAnsiToUtf8(keys);
        }

        private static string[] EncodeAnsiToUtf8(string[] keys)
        {
            var utf8 = Encoding.UTF8;
            var ansi = Encoding.GetEncoding(1252);

            for (int i = 0; i < keys.Length; i++)
                keys[i] = utf8.GetString(ansi.GetBytes(keys[i]));

            return keys;
        }

        private static void EnsureDirectoryExists(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        [Serializable]
        private class PlayerPrefEntry
        {
            public string key;
            public string value;

            public PlayerPrefEntry(string key, string value)
            {
                this.key = key;
                this.value = value;
            }
        }

        [Serializable]
        private class PlayerPrefsData
        {
            public List<PlayerPrefEntry> prefs = new();
        }
    }
}
#endif

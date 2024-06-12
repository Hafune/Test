using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Lib.Editor
{
    public static class GitPackageInstaller
    {
        private static readonly List<string> Packages = new()
        {
            "https://github.com/dbrizov/NaughtyAttributes.git#upm",
            "https://github.com/Leopotam/ecslite-di.git",
            "https://github.com/Leopotam/ecslite-unityeditor.git",
            "git@github.com:Hafune/ecslite.git",
            "com.unity.cinemachine",
            "com.unity.inputsystem"
            // Добавьте сюда другие ссылки на пакеты
        };

        private static AddRequest _addRequest;
        private static Queue<string> _packagesToInstall;
        private static string _currentPackage;

        [MenuItem("Tools/Install Packages")]
        public static void InstallPackages()
        {
            var manifestPath = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            var manifestJson = File.ReadAllText(manifestPath);
            var manifest = JObject.Parse(manifestJson);
            var dependencies = (JObject)manifest["dependencies"]!;

            var existingPackages = new HashSet<string>(dependencies.Properties().SelectMany(p =>
                new[]
                {
                    p.Name, p.Value.ToString()
                }));
            _packagesToInstall = new Queue<string>();

            foreach (var url in Packages)
            {
                if (!existingPackages.Contains(url))
                {
                    _packagesToInstall.Enqueue(url);
                }
            }

            if (_packagesToInstall.Any())
            {
                InstallNextPackage();
            }
            else
            {
                Debug.Log("All packages are already installed.");
            }
        }

        private static void InstallNextPackage()
        {
            if (_packagesToInstall.Count == 0)
            {
                Debug.Log("All packages have been installed.");
                return;
            }

            _currentPackage = _packagesToInstall.Dequeue();
            _addRequest = Client.Add(_currentPackage);
            EditorApplication.update += CheckAddRequest;
        }

        private static void CheckAddRequest()
        {
            if (!_addRequest.IsCompleted) 
                return;

            EditorApplication.update -= CheckAddRequest;

            if (_addRequest.Status == StatusCode.Failure)
            {
                Debug.LogError("Failed to add package: " + _currentPackage + " - " + _addRequest.Error.message);
            }
            else
            {
                Debug.Log("Successfully added package: " + _currentPackage);
            }

            InstallNextPackage();
        }
    }
}
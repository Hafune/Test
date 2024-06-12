using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine.UIElements;

namespace Lib
{
    public static class UxmlNode
    {
        public static VisualTreeAsset loadElem([CallerFilePath] string path = null) =>
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(CalcPath(path));

        private static string CalcPath(string path) =>
            Regex.Replace(path!, @"^(.+?)(Assets.+)", "$2").Replace(".cs", ".uxml");
    }
}


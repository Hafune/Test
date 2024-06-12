using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    internal static class TemplateUtility
    {
        public static string ReplaceTagWithIndentedMultiline(string templateContent, string tag,
            IEnumerable<string> newLines)
        {
            var tagWithIndentRegex = $"\\ *{tag}";
            var match = Regex.Match(templateContent, tagWithIndentRegex);
            //var indent = match.Value[..^tag.Length];
            var indent = match.Value.Substring(0, match.Value.Length - tag.Length);
            var replacement = string.Join(Environment.NewLine + indent, newLines);
            return Regex.Replace(templateContent, tag, replacement);
        }

        public static string ReplaceClassNameTag(string templateContent, string className)
        {
            return templateContent.Replace("#SCRIPT_NAME#", className);
        }

        public static string ReplacePackageVersionNameTag(string templateContent, string packageVersion)
        {
            return templateContent.Replace("#PACKAGE_VERSION#", packageVersion);
        }

        public static string ReplaceNamespaceTags(string templateContent, string namespaceName)
        {
            const string namespaceStartTag = "#NAMESPACE_START#";
            const string namespaceEndTag = "#NAMESPACE_END#";
            const string endOfLineAndSpaces = @"((\r\n)|\n)\ *";

            if (!templateContent.Contains(namespaceStartTag) || !templateContent.Contains(namespaceEndTag))
                return templateContent;

            if (string.IsNullOrEmpty(namespaceName))
            {
                templateContent = Regex.Replace(templateContent, $"{endOfLineAndSpaces}{namespaceStartTag}",
                    string.Empty);
                templateContent = Regex.Replace(templateContent, $"{endOfLineAndSpaces}{namespaceEndTag}",
                    string.Empty);

                return templateContent;
            }

            var sb = new StringBuilder();
            var namespaceIndent = string.Empty;
            using (var sr = new StringReader(templateContent))
            {
                while (sr.ReadLine() is { } line)
                {
                    if (line.Contains(namespaceStartTag))
                    {
                        //namespaceIndent = line[..^namespaceStartTag.Length];
                        namespaceIndent = line.Substring(0, line.Length - namespaceStartTag.Length);
                        sb.AppendLine($"namespace {namespaceName}");
                        sb.AppendLine("{");
                        continue;
                    }

                    if (line.Contains(namespaceEndTag))
                    {
                        sb.AppendLine(line.Replace(namespaceEndTag, "}"));
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(line))
                        sb.Append(namespaceIndent);
                    sb.AppendLine(line);
                }
            }

            return sb.ToString();
        }

        public static bool IsEqualWithoutComments(string oldContent, string newContent)
        {
            return TrimStartComments(oldContent) == TrimStartComments(newContent);
        }

        public static string TrimStartComments(string content)
        {
            using var reader = new StringReader(content);
            while ((char)reader.Peek() == '/')
                reader.ReadLine();
            return reader.ReadToEnd();
        }

        public static string FindExactAssetPath(string fileName, string ext, string query = "t:Script")
        {
            var guids = AssetDatabase.FindAssets($"{query} {fileName}");
            string fullNameWithExt = fileName + ext;

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid).Replace("Assets", "");

                if (Path.GetFileName(path) == fullNameWithExt)
                    return path;
            }

            throw new Exception($"Ассет не был найден: {fullNameWithExt}");
        }

        public static string ReadFile(string path) => File.ReadAllText(Application.dataPath + path);

        // [Obsolete("Использовать WriteScriptIfDifferent, передавать точный адрес файла")]
        // public static bool WriteScriptIfDifferentOld(string content, string fileName)
        // {
        //     string path = FindExactAssetPath(fileName, ext: ".cs");
        //     string filePath = Application.dataPath + path;
        //     string fileContent = File.ReadAllText(filePath);
        //
        //     if (IsEqualWithoutComments(fileContent, content))
        //         return false;
        //
        //     File.WriteAllText(filePath, content);
        //     return true;
        // }

        public static bool WriteScriptIfDifferent(string content, string assetFilePath)
        {
            var fullPath = GetFullPath(assetFilePath);

            if (File.Exists(fullPath) && IsEqualWithoutComments(File.ReadAllText(fullPath), content))
                return false;

            File.WriteAllText(fullPath, content);
            return true;
        }

        private static string GetFullPath(string fileName) => $"{Application.dataPath}{fileName}";
    }
}
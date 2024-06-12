using System.Diagnostics;
using System.Text;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Core.Editor
{
    [InitializeOnLoad]
    public class GenLauncher
    {
        public static string DIR = "/Core/_GeneratedFiles/";
        public static string EDITOR_DIR = "/Core/Editor/GeneratedLinks/";
        public static string MakePath(string name) => $"{DIR}{name}.cs";
        public static string MakeEditorPath(string name) => $"{EDITOR_DIR}{name}.cs";
        
        static GenLauncher()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
                return;

            var stopWatch = Stopwatch.StartNew();
            int errorRepeatCount = 4;
            int repeatCount = 0;

            while (repeatCount++ < errorRepeatCount &&
                   GenActionEnum.Gen() |
                   GenValueEnum.Gen() |
                   GenSlotTagEnum.Gen() |
                   // GenAnimatorBehaviorData.Gen() |
                   GenActionSystemsSchema.Gen() |
                   GenComponentPools.Gen() |
                   GenValuePoolsUtility.Gen() |
                   GenSlotValueTypes.Gen() |
                   GenSlotFilterAndPools.Gen() |
                   GenSlotSetupSystemsNode.Gen() |
                   GenInitValuesFromBaseValuesSystem.Gen() |
                   GenInitRecalculateAllValuesSystem.Gen() |
                   GenEventStartRecalculateValueSystem.Gen() |
                   GenDelEventStartRecalculateValue.Gen() |
                   GenEventRefreshValueBySlotSystem.Gen() |
                   GenValueScaleSystemsUtility.Gen())
            {
                AssetDatabase.Refresh();
            }

            stopWatch.Stop();
            Debug.Log(
                $"CodeGenLauncher StopWatch.Elapsed: {FormatUiValuesUtility.FloatDim2((float)stopWatch.Elapsed.TotalSeconds)} Seconds, repeats {repeatCount}/{errorRepeatCount}");

            if (repeatCount == errorRepeatCount)
                Debug.LogWarning($"Слишком много ({errorRepeatCount}) проходов генерации кода !!!");
        }

        [MenuItem("Auto/Remove Generated Values Logic")]
        private static void RemoveGeneratedCode()
        {
            GenValuePoolsUtility.RemoveGen();
            GenSlotValueTypes.RemoveGen();
            GenSlotFilterAndPools.RemoveGen();
            GenSlotSetupSystemsNode.RemoveGen();
            GenInitValuesFromBaseValuesSystem.RemoveGen();
            GenInitRecalculateAllValuesSystem.RemoveGen();
            GenEventStartRecalculateValueSystem.RemoveGen();
            GenDelEventStartRecalculateValue.RemoveGen();
            GenEventRefreshValueBySlotSystem.RemoveGen();
            GenValueScaleSystemsUtility.RemoveGen();
        }
    }
}
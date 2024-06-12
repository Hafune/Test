using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Lib.Editor
{
    public class LayerSettingsManager
    {
        private const string FilePath = "Assets/LayerSettings.json";

        [MenuItem("Tools/Export Layer Settings")]
        public static void ExportLayers()
        {
            var layerData = new LayerData
            {
                layers = new List<string>(),
                collisions3D = new List<CollisionData>(),
                collisions2D = new List<CollisionData>()
            };

            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                layerData.layers.Add(layerName);
            }

            for (int i = 0; i < 32; i++)
            {
                for (int j = i; j < 32; j++)
                {
                    if (!Physics.GetIgnoreLayerCollision(i, j))
                    {
                        layerData.collisions3D.Add(new CollisionData
                        {
                            layerA = LayerMask.LayerToName(i),
                            layerB = LayerMask.LayerToName(j)
                        });
                    }

                    if (!Physics2D.GetIgnoreLayerCollision(i, j))
                    {
                        layerData.collisions2D.Add(new CollisionData
                        {
                            layerA = LayerMask.LayerToName(i),
                            layerB = LayerMask.LayerToName(j)
                        });
                    }
                }
            }

            string json = JsonUtility.ToJson(layerData, true);
            File.WriteAllText(FilePath, json);
            Debug.Log("Layer settings exported to " + FilePath);
        }

        [MenuItem("Tools/Import Layer Settings")]
        public static void ImportLayers()
        {
            if (!File.Exists(FilePath))
            {
                Debug.LogError("Layer settings file not found at " + FilePath);
                return;
            }

            string json = File.ReadAllText(FilePath);
            var layerData = JsonUtility.FromJson<LayerData>(json);

            // Создаем недостающие слои
            for (int i = 0; i < layerData.layers.Count; i++)
            {
                CreateLayer(layerData.layers[i], i);
            }

            // Сбрасываем все коллизии для 3D и 2D
            ResetAllLayerCollisions();

            // Устанавливаем коллизии для 3D
            foreach (var collision in layerData.collisions3D)
            {
                int layerA = LayerMask.NameToLayer(collision.layerA);
                int layerB = LayerMask.NameToLayer(collision.layerB);

                if (layerA != -1 && layerB != -1)
                {
                    Physics.IgnoreLayerCollision(layerA, layerB, false);
                }
                else
                {
                    Debug.LogWarning($"Layer not found for collision: {collision.layerA}, {collision.layerB}");
                }
            }

            // Устанавливаем коллизии для 2D
            foreach (var collision in layerData.collisions2D)
            {
                int layerA = LayerMask.NameToLayer(collision.layerA);
                int layerB = LayerMask.NameToLayer(collision.layerB);

                if (layerA != -1 && layerB != -1)
                {
                    Physics2D.IgnoreLayerCollision(layerA, layerB, false);
                }
                else
                {
                    Debug.LogWarning($"Layer not found for collision: {collision.layerA}, {collision.layerB}");
                }
            }

            Debug.Log("Layer settings imported from " + FilePath);
        }

        private static void CreateLayer(string layerName, int index)
        {
            if (string.IsNullOrEmpty(layerName))
                return;

            SerializedObject tagManager =
                new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layersProp = tagManager.FindProperty("layers");

            if (layersProp == null || layersProp.arraySize <= index)
            {
                Debug.LogError("Failed to find layers property in TagManager.");
                return;
            }

            SerializedProperty layerProp = layersProp.GetArrayElementAtIndex(index);

            if (layerProp != null && string.IsNullOrEmpty(layerProp.stringValue))
            {
                layerProp.stringValue = layerName;
                tagManager.ApplyModifiedProperties();
                Debug.Log($"Layer '{layerName}' created at index {index}.");
            }
        }

        private static void ResetAllLayerCollisions()
        {
            int layerCount = 32; // В Unity всего 32 слоя

            // Сбрасываем все коллизии для 3D и 2D
            for (int i = 0; i < layerCount; i++)
            {
                for (int j = i; j < layerCount; j++)
                {
                    Physics.IgnoreLayerCollision(i, j, true);
                    Physics2D.IgnoreLayerCollision(i, j, true);
                }
            }
        }

        [System.Serializable]
        public class LayerData
        {
            public List<string> layers;
            public List<CollisionData> collisions3D;
            public List<CollisionData> collisions2D;
        }

        [System.Serializable]
        public class CollisionData
        {
            public string layerA;
            public string layerB;
        }
    }
}
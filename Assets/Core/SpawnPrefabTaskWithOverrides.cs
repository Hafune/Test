using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Core.Lib;
using Core.Services;
using Lib;
using Reflex;
using Unity.VisualScripting;
using UnityEngine;
using VInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core
{
    [ExecuteInEditMode]
    public class SpawnPrefabTaskWithOverrides : MonoConstruct, IMyTask
    {
        private static readonly Dictionary<int, Type> SerializableTypes = new()
        {
            { 1, typeof(GemInstance) },
            { 2, typeof(KeySilverInstance) },
        };

        private static Dictionary<int, Type> TypesCache = new();
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> PropertiesInfo = new();
        private static Dictionary<Type, Dictionary<string, FieldInfo>> FieldsInfo = new();
        private static Dictionary<Type, TypeConverter> TypeConverters = new();
        private static Dictionary<Type, HashSet<string>> TypeVariableNames = new();

        private static BindingFlags BindingFlags = BindingFlags.Instance |
                                                   BindingFlags.NonPublic |
                                                   BindingFlags.Public |
                                                   BindingFlags.GetField |
                                                   BindingFlags.GetProperty;

        [SerializeField] private Transform _prefab;
        [SerializeField] private bool _runSelfOnStart;
        [SerializeField] private bool _useDontDestroyPool;
        [SerializeField] private SerializedProperties[] _overrideProperties;
        private Context _context;
        public bool InProgress => false;
        private GameObject _container;

        protected override void Construct(Context context) => _context = context;

#if UNITY_EDITOR
        private GameObject _instance;
        private Transform _lastInstance;

        private void OnValidate()
        {
            if (Application.isPlaying)
                return;

            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;

            if (_lastInstance != _prefab)
                EditorApplication.delayCall += Refresh;
        }

        [Button]
        private void AutoRename()
        {
            if (_prefab)
                name = _prefab.name + "_spawn";
        }

        [Button]
        [ContextMenu("Serialize Property Modifications")]
        public void SerializePropertyModifications()
        {
            if (!_instance)
                return;

            var properties = PrefabUtility.GetPropertyModifications(_instance);
            Dictionary<int, SerializedProperties> props = new();

            properties
                .Where(p => SerializableTypes.ContainsValue(p.target.GetType()) &&
                            (p.propertyPath.Contains("BackingField") ||
                             !p.propertyPath.StartsWith("m_")))
                .DistinctBy(p =>
                {
                    string path;
                    if (!p.propertyPath.Contains("BackingField"))
                    {
                        path = Regex.Match(p.propertyPath, @"(.+?)\..+").Groups[1].Value;
                        path = string.IsNullOrEmpty(path) ? p.propertyPath : path;
                    }
                    else
                    {
                        path = Regex.Match(p.propertyPath, "<(.+)>").Groups[1].Value;
                    }

                    return path;
                })
                .ForEach(p =>
                {
                    var key = SerializableTypes.First(pair => pair.Value == p.target.GetType()).Key;
                    //var key =  p.target.GetType().AssemblyQualifiedName!;

                    if (!props.ContainsKey(key))
                    {
                        props.Add(key, new SerializedProperties
                        {
                            typeID = key,
                            properties = new()
                        });
                    }

                    string path;
                    object value;
                    var script = _instance.GetComponent(p.target.GetType());

                    if (!p.propertyPath.Contains("BackingField"))
                    {
                        path = Regex.Match(p.propertyPath, @"(.+?)\..+").Groups[1].Value;
                        path = string.IsNullOrEmpty(path) ? p.propertyPath : path;
                        var field = p.target.GetType().GetField(path, BindingFlags)!;
                        value = field.GetValue(script);
                    }
                    else
                    {
                        path = Regex.Match(p.propertyPath, "<(.+)>").Groups[1].Value;
                        var field = p.target.GetType().GetProperty(path, BindingFlags)!;
                        value = field.GetValue(script);
                    }

                    var type = value.GetType();
                    var stringValue = type.IsBasic() ? value.ToString() : JsonUtility.ToJson(value);

                    props[key].properties.Add(
                        new SerializedProperty
                        {
                            propertyPath = path,
                            value = stringValue
                        });
                });

            _overrideProperties = props.Values.ToArray();
        }

        private void Refresh()
        {
            if (this.IsDestroyed() || !enabled)
                return;

            OnDisable();
            OnEnable();
        }

        private void OnEnable()
        {
            if (this.IsDestroyed() || Application.isPlaying || !_prefab)
                return;

            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;

            _lastInstance = _prefab;

            transform.DestroyChildren();
            _instance = SpawnPrefabTask.InstantiatePrefabDontSave(_prefab, transform);
            ApplyPropertyModifications(_instance);
        }

        private void OnDisable()
        {
            if (Application.isPlaying)
                return;

            transform.DestroyChildren();

            _instance = null;
            _lastInstance = null;
        }

        [Button]
        [ContextMenu("Apply Property Modifications")]
        private void ApplyPropertyModifications() => ApplyPropertyModifications(_instance);
#endif

        private void Awake()
        {
            if (!Application.isPlaying)
                return;

            _container = new GameObject();
            _container.SetActive(false);
            _container.transform.parent = transform;
            _container.transform.localScale = Vector3.one;
            _container.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        private void Start()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            if (_runSelfOnStart)
                Begin(_context, null);
        }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            var effectPool = _useDontDestroyPool
                ? context.Resolve<PoolService>().DontDisposablePool.GetPullByPrefab(_prefab)
                : context.Resolve<PoolService>().ScenePool.GetPullByPrefab(_prefab);
            
            var instance = effectPool.GetObject(transform.position, transform.rotation, _container.transform);
            instance.localScale = Vector3.one;
            
            ApplyPropertyModifications(instance.gameObject);

            if (_useDontDestroyPool)
            {
                instance.parent = null;
                DontDestroyOnLoad(instance.gameObject);
            }
            else
            {
                instance.parent = transform;
            }

            if (!_runSelfOnStart)
                payload.Set(instance);

            onComplete?.Invoke(this);
        }

        private void ApplyPropertyModifications(GameObject instance)
        {
            if (!instance)
                return;

            for (int i = 0, iMax = _overrideProperties.Length; i < iMax; i++)
            {
                var property = _overrideProperties[i];
                var _type = SerializableTypes[property.typeID];

                if (TypesCache.TryGetValue(property.typeID, out var type))
                {
                    ApplyProperty(instance, type, property);
                    return;
                }

                TypesCache.Add(property.typeID, _type);
                ApplyProperty(instance, _type, property);
            }
        }

        private void ApplyProperty(GameObject instance, Type type, SerializedProperties property)
        {
            if (property.properties.Count == 0)
                return;

            var component = instance.GetComponent(type);

            if (!TypeVariableNames.TryGetValue(type, out var set) ||
                property.properties.Any(p => set.Add(p.propertyPath)))
            {
                if (!FieldsInfo.TryGetValue(type, out _))
                    FieldsInfo.Add(type, new());

                if (!PropertiesInfo.TryGetValue(type, out _))
                    PropertiesInfo.Add(type, new());

                set ??= new();
                TypeVariableNames.Add(type, set);

                foreach (var prop in property.properties)
                {
                    FieldsInfo[type].Add(prop.propertyPath, type.GetField(prop.propertyPath, BindingFlags));
                    PropertiesInfo[type].Add(prop.propertyPath, type.GetProperty(prop.propertyPath, BindingFlags));
                    set.Add(prop.propertyPath);
                }
            }

            for (int i = 0, iMax = property.properties.Count; i < iMax; i++)
            {
                var prop = property.properties[i];
                var fieldInfo = FieldsInfo[type][prop.propertyPath];
                var propertyInfo = PropertiesInfo[type][prop.propertyPath];
                var converterKey = fieldInfo?.FieldType ?? propertyInfo!.PropertyType;

                if (!TypeConverters.TryGetValue(converterKey, out var converter))
                {
                    converter = TypeDescriptor.GetConverter(converterKey);
                    TypeConverters.Add(converterKey, converter);
                }

                var value = prop.value.Length > 0 && prop.value[0] == '{'
                    ? JsonUtility.FromJson(prop.value, converterKey)
                    : converter.ConvertFrom(prop.value);

                fieldInfo?.SetValue(component, value);
                propertyInfo?.SetValue(component, value);
            }
#if UNITY_EDITOR
            PrefabUtility.RecordPrefabInstancePropertyModifications(instance);
#endif
        }

        [Serializable]
        private struct SerializedProperties
        {
            public int typeID;
            public List<SerializedProperty> properties;
        }

        [Serializable]
        private struct SerializedProperty
        {
            public string propertyPath;
            public string value;
        }
    }
}
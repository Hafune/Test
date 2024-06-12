#if UNITY_EDITOR
using System;
using System.Collections;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using VInspector;

public class EditorBatchFilter : MonoBehaviour
{
    [SerializeField] internal GameObject _reference;
    [SerializeField] private bool _includeInactive;
    [SerializeField] internal bool _addIgnorePrefabs = true;
    [SerializeField] internal bool _addDeepCompare;
    [SerializeField] internal bool _addCompareName;
    [SerializeField] internal bool _addCompareChildCount;
    [SerializeField] internal bool _addCompareComponentCount;
    [EndIf] private AbstractEditorBatchExecute _execute;
    private Action _next;
    private Action _cancel;

    private void OnValidate() => EditorApplication.delayCall += Refresh;

    public void RunProcessing(Action next, Action cancel)
    {
        _next = next;
        _cancel = cancel;
        _execute = GetComponent<AbstractEditorBatchExecute>();

        if (_execute is null)
        {
            Debug.LogWarning("Не задан екзекутор");
            _next?.Invoke();
            return;
        }

        EditorCoroutineUtility.StartCoroutine(RunProcessingPrivate(), gameObject);
    }

    private void Refresh()
    {
        if (!_reference)
            return;

        RemoveFilters();

        var components = _reference.GetComponents<Component>();

        if (TryGetComponent<EditorBatchReplace>(out var r))
        {
            r.prefab = _reference;
            r.Rename();
        }

        if (!_addIgnorePrefabs)
            return;

        foreach (var c in components)
            switch (c)
            {
                case Transform:
                {
                    break;
                }
                case MeshFilter co:
                {
                    var compare = gameObject.AddComponent<CompareMeshFilter>();
                    compare.mesh = co.sharedMesh;
                    break;
                }
                case MeshRenderer co:
                {
                    var compare = gameObject.AddComponent<CompareMeshRenderer>();
                    compare.material = co.sharedMaterial;
                    break;
                }
                case Animator co:
                {
                    var compare = gameObject.AddComponent<CompareAnimator>();
                    compare.controller = co.runtimeAnimatorController;
                    break;
                }
                case ParticleSystem co:
                {
                    var compare = gameObject.AddComponent<CompareParticle>();
                    compare.particle = co;
                    break;
                }
                case ParticleSystemRenderer co:
                {
                    break;
                }
                default:
                {
                    var compare = gameObject.AddComponent<CompareComponent>();
                    compare.AssemblyQualifiedName = c.GetType().AssemblyQualifiedName;
                    break;
                }
            }


        if (_addCompareName)
        {
            var compare = gameObject.AddComponent<CompareNameStartsWith>();
            compare.startsWith = _reference.name;
        }

        if (_addCompareChildCount)
        {
            var compare = gameObject.AddComponent<CompareChildCount>();
            compare.count = _reference.transform.childCount;
        }

        if (_addCompareComponentCount)
        {
            var compare = gameObject.AddComponent<CompareComponentCount>();
            compare.count = _reference.GetComponents<Component>().Length;
        }

        if (_addIgnorePrefabs)
        {
            gameObject.AddComponent<CompareIsNotPartOfAnyPrefab>();
        }

        if (_addDeepCompare)
        {
            gameObject.AddComponent<CompareDeep>().MakeFiltersForChildren(_reference);
        }

        _reference = null;
    }

    private Transform[] Filter(Transform root)
    {
        var gameObjects = root.GetComponentsInChildren<Transform>(_includeInactive).Select(t => t.gameObject);
        var filters = GetComponents<AbstractCompare>().Where(f => f.enabled).ToArray();

        return filters.Length == 0
            ? new[] { root }
            : (from go in gameObjects where filters.All(f => f.Compare(go)) select go.transform)
            .ToArray();
    }

    [Button]
    [ButtonSize(22)]
    private void RemoveFilters()
    {
        foreach (var t in GetComponents<AbstractCompare>())
            DestroyImmediate(t);
    }

    private IEnumerator RunProcessingPrivate()
    {
        yield return null;
        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        var root = prefabStage.prefabContentsRoot;

        var children = Filter(root.transform);

        if (children.Length == 0)
        {
            _next();
            yield break;
        }

        Debug.Log($"Процессор {name}, кол-во {children.Length} в {root.name}");

        _execute.Execute(children, _next, _cancel);
    }
}
#endif
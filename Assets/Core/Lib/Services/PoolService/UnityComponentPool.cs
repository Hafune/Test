using Core.Lib;
using Reflex;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class UnityComponentPool<T> : IPool where T : Component
{
    private T _prefab;
    private int _denseItemsCount;
    private MyList<T> _denseItems = new();
    private MyList<int> _sparseItemIndexes = new();
    private Context _context;
    private readonly bool _dontDestroyOnLoad;
    private GameObject _container;

    internal UnityComponentPool(Context context, T prefab, bool dontDestroyOnLoad = false)
    {
        _context = context;
        _prefab = prefab;
        _dontDestroyOnLoad = dontDestroyOnLoad;
        _container = new GameObject("Pool Container");
        _container.SetActive(false);

        if (_dontDestroyOnLoad)
            Object.DontDestroyOnLoad(_container);
    }

    public T GetObject(Vector3 position, Quaternion quaternion, Transform parent = null)
    {
        T effect = null;

        if (_denseItemsCount > 0)
            effect = _denseItems[_sparseItemIndexes[--_denseItemsCount]];

        effect ??= BuildObject(position, quaternion, parent);
        effect.transform.SetParent(parent, false);
        effect.transform.SetPositionAndRotation(position, quaternion);
        effect.gameObject.SetActive(true);

        return effect;
    }

    private T BuildObject(Vector3 position, Quaternion quaternion, Transform parent = null)
    {
        var obj = _context.Instantiate(_prefab, position, quaternion, parent);
        var dispatcher = obj.gameObject.AddComponent<EnableDispatcher>();
        var index = _denseItems.Count;
        dispatcher.index = index;
        dispatcher.OnDisabled += () => ReturnInPull(index);

        _denseItems.Add(obj);

        if (_dontDestroyOnLoad && !parent)
            Object.DontDestroyOnLoad(obj.gameObject);

        return obj;
    }

    private void ReturnInPull(int index)
    {
        if (_denseItemsCount >= _sparseItemIndexes.Count)
            _sparseItemIndexes.Add(index);
        else
            _sparseItemIndexes[_denseItemsCount] = index;

        _denseItems[_sparseItemIndexes[_denseItemsCount]].transform.parent = _container.transform;
        _denseItemsCount++;
    }

    public void ForceReturnInPool()
    {
        foreach (var item in _denseItems)
            item.gameObject.SetActive(false);
    }

    public void Dispose()
    {
        foreach (var item in _denseItems)
            if (!item.IsDestroyed())
                Object.Destroy(item.gameObject);

        _denseItems.Clear();
        _sparseItemIndexes.Clear();
        _denseItemsCount = 0;
    }
}
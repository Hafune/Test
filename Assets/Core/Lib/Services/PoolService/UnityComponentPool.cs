using Core.Lib;
using Reflex;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class UnityComponentPool<T> : IPool where T : Component
{
    private T _prefab;
    private MyList<T> _pool = new();
    private MyList<GameObject> _gameObjectPool = new();
    private Context _context;
    private readonly bool _dontDestroyOnLoad;

    internal UnityComponentPool(Context context, T prefab, bool dontDestroyOnLoad = false)
    {
        _context = context;
        _prefab = prefab;
        _dontDestroyOnLoad = dontDestroyOnLoad;
    }

    public T GetObject(Vector3 position, Quaternion quaternion, Transform parent = null)
    {
        T effect = null;

        for (int i = 0, count = _gameObjectPool.Count; i < count; i++)
        {
            if (_gameObjectPool.Items[i].activeSelf)
                continue;

            effect = _pool.Items[i];
            break;
        }

        if (effect is null)
            return BuildObject(position, quaternion, parent);

        effect.transform.SetParent(parent, false);
        effect.transform.SetPositionAndRotation(position, quaternion);
        effect.gameObject.SetActive(true);

        return effect;
    }

    private T BuildObject(Vector3 position, Quaternion quaternion, Transform parent = null)
    {
        var newEffect = _context.Instantiate(_prefab, position, quaternion, parent);
        _pool.Add(newEffect);
        _gameObjectPool.Add(newEffect.gameObject);

        if (_dontDestroyOnLoad && !parent)
            Object.DontDestroyOnLoad(newEffect.gameObject);

        newEffect.transform.SetParent(parent, false);
        newEffect.transform.SetPositionAndRotation(position, quaternion);
        newEffect.gameObject.SetActive(true);

        return newEffect;
    }

    public void ForceReturnInPool()
    {
        foreach (var item in _pool)
            item.gameObject.SetActive(false);
    }

    public void Dispose()
    {
        foreach (var item in _pool)
            if (!item.IsDestroyed())
                Object.Destroy(item.gameObject);

        _pool.Clear();
        _gameObjectPool.Clear();
    }
}
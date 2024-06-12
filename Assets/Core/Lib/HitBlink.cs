using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitBlink : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> _skinnedMeshRenderer;
    [SerializeField] HitBlinkData _hitBlinkData;
    private MyCoroutine _coroutine;
    private WaitForSeconds _wait;
    private int _materialHash;

    private void OnValidate() =>
        _skinnedMeshRenderer =
            _skinnedMeshRenderer.Count > 0 && _skinnedMeshRenderer.All(i => i != null)
                ? _skinnedMeshRenderer
                : GetComponentsInChildren<SkinnedMeshRenderer>().ToList();

    private void Awake()
    {
        _wait = new WaitForSeconds(_hitBlinkData.time);
        _coroutine = new MyCoroutine(this, BeginBlink);
        _materialHash = _hitBlinkData.material.ComputeCRC();
    }

    private void OnDisable()
    {
        _coroutine.StopCoroutine();
        Clear();
    }

    public void Activate() => _coroutine.StartCoroutine(false);

    private IEnumerator BeginBlink()
    {
        for (int i = 0; i < _skinnedMeshRenderer.Count; i++)
        {
            var renderer = _skinnedMeshRenderer[i];
            renderer.materials =
                renderer.materials.Append(_hitBlinkData.material).ToArray();
        }

        yield return _wait;

        Clear();
    }

    private void Clear()
    {
        for (int i = 0; i < _skinnedMeshRenderer.Count; i++)
        {
            var renderer = _skinnedMeshRenderer[i];
            renderer.materials =
                renderer.materials.Where(m => m.ComputeCRC() != _materialHash).ToArray();
        }
    }
}
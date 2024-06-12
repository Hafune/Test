using System;
using System.Collections;
using UnityEngine;

public class MyCoroutine
{
    private Coroutine _coroutine;
    private MonoBehaviour _owner;
    private bool _isRunning;
    private Func<IEnumerator> _action;

    public bool IsRunning => _isRunning;

    public MyCoroutine(MonoBehaviour owner, Func<IEnumerator> action)
    {
        _owner = owner;
        _action = action;
    }

    public void StartCoroutine(bool recreateIfStarted = true)
    {
        if (recreateIfStarted && _isRunning)
        {
            _owner.StopCoroutine(_coroutine);
            _isRunning = false;
        }

        if (!_isRunning)
            _coroutine = _owner.StartCoroutine(Watch(_action.Invoke()));
    }

    public void StopCoroutine()
    {
        if (ReferenceEquals(_coroutine, null))
            return;

        _owner.StopCoroutine(_coroutine);
        _isRunning = false;
    }

    private IEnumerator Watch(IEnumerator enumerator)
    {
        _isRunning = true;
        yield return enumerator;
        _isRunning = false;
    }
}
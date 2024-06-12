using System;
using System.Collections;
using Lib;
using UnityEngine;

public class CoroutineFloatValueFollower
{
    public float Currentvalue;
    public float Targetvalue;
    public float Speed;
    
    private event Action _onComplete;
    private event Action<float> _onChange;
    private MonoBehaviour _monoBehaviour;
    private bool _coroutineIsRunning;

    public CoroutineFloatValueFollower(MonoBehaviour monoBehaviour, float currentValue = 0, float speed = 1,
        Action<float> callback = null)
    {
        _monoBehaviour = monoBehaviour;
        Speed = speed;
        Currentvalue = currentValue;

        if (callback != null)
            SetOnChange(callback);
    }

    public void SetOnChange(Action<float> call)
    {
        _onChange = null;
        _onChange += call;
    }

    public void OnComplete(Action call)
    {
        _onComplete = null;
        _onComplete += call;
    }

    public void StartFollowFor(float value)
    {
        Targetvalue = value;

        if (_coroutineIsRunning)
            return;

        _coroutineIsRunning = true;
        _monoBehaviour.StartCoroutine(FollowForTargetValue());
    }

    public IEnumerator FollowForTargetValue()
    {
        while (Currentvalue != Targetvalue)
        {
            Currentvalue = Mathf.MoveTowards(Currentvalue, Targetvalue, Time.deltaTime * Speed);
            _onChange?.Invoke(Currentvalue);
            yield return null;
        }

        _coroutineIsRunning = false;
        _onComplete?.Invoke();
    }
}
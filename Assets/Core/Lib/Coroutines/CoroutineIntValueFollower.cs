using System;
using System.Collections;
using Lib;
using UnityEngine;

public class CoroutineIntValueFollower
{
    public float Currentvalue;
    public int Targetvalue;
    public float Speed;
    public event Action OnComplete;

    private event Action<int> OnChange;
    private MonoBehaviour _monoBehaviour;
    private bool _coroutineIsRunning;

    public CoroutineIntValueFollower(MonoBehaviour monoBehaviour, float currentValue = 0, float speed = 1,
        Action<int> callback = null)
    {
        _monoBehaviour = monoBehaviour;
        Speed = speed;
        Currentvalue = currentValue;

        if (callback != null)
            SetOnChange(callback);
    }

    public void SetOnChange(Action<int> call)
    {
        OnChange = null;
        OnChange += call;
    }

    public void StartFollowFor(int value)
    {
        Targetvalue = value;

        if (_coroutineIsRunning)
            return;

        _coroutineIsRunning = true;
        _monoBehaviour.StartCoroutine(FollowForTargetValue());
    }

    public IEnumerator FollowForTargetValue()
    {
        int lastValue = (int) Currentvalue;

        while (lastValue != Targetvalue)
        {
            Currentvalue = Mathf.MoveTowards(Currentvalue, Targetvalue, Time.deltaTime * Speed);

            if (lastValue != (int) Currentvalue)
            {
                lastValue = (int) Currentvalue;
                OnChange?.Invoke(lastValue);
            }

            yield return null;
        }

        _coroutineIsRunning = false;
        OnComplete?.Invoke();
    }
}
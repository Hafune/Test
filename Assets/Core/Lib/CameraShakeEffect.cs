using System.Collections;
using Cinemachine;
using Core.Lib;
using UnityEngine;

public class CameraShakeEffect : AbstractEffect
{
    [SerializeField] private float _maxTime;
    [SerializeField] private float _maxIntensity;
    [SerializeField] private bool _spawnOnEnable;
    private CinemachineBasicMultiChannelPerlin _channel;
    private Coroutine _updateCoroutine;
    private float _time;

    private void Awake() => _channel = Context.Resolve<CinemachineVirtualCamera>()
        .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    private void OnEnable()
    {
        if (_spawnOnEnable)
            Execute();
    }

    private void OnDisable()
    {
        if (_updateCoroutine is not null)
            StopCoroutine(_updateCoroutine);

        Clear();
    }

    private void Clear()
    {
        _updateCoroutine = null;
        _channel.m_AmplitudeGain = 0;
    }

    public override void Execute()
    {
        _time = _maxTime;
        _updateCoroutine ??= StartCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        while (_time > 0)
        {
            _time -= Time.unscaledDeltaTime;
            _channel.m_AmplitudeGain = Mathf.Lerp(_maxIntensity, 0f, _time / _maxTime);
            yield return null;
        }

        Clear();
    }
}
using Cinemachine;
using UnityEngine;

public class TestShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _maxTime;
    [SerializeField] private float _maxIntensity;
    [SerializeField] private bool _refresh;
    private CinemachineBasicMultiChannelPerlin _channel;
    private float _time;
    private bool _hasShake;

    
    private void OnValidate()
    {
        if (!_refresh)
            return;

        _refresh = false;
        _hasShake = true;
        _time = _maxTime;
    }

    private void Start()
    {
        _channel = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (!_hasShake)
            return;

        _time -= Time.unscaledDeltaTime;
        _channel.m_AmplitudeGain = Mathf.Lerp(_maxIntensity, 0f, _time / _maxTime);

        if (_time > 0)
            return;

        _hasShake = false;
        _channel.m_AmplitudeGain = 0;
    }
}
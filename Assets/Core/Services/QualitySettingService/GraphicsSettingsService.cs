using System;
using System.Globalization;
using Cinemachine;
using Core.Services;
using Reflex;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core
{
    public class GraphicsSettingsService : IInitializableService, ISerializableService
    {
        public event Action<bool> OnSsaoChange;
        public event Action<bool> OnShadowChange;
        public event Action<float> OnDrawingDistanceChange;
        public event Action<float> OnRenderScaleChange;

        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;
        private SdkService _sdkService;
        private float _defaultShadowsDistance;
        private UniversalRenderPipelineAsset _urpAsset;
        private bool _defaultSsaoEnabled;
        private CinemachineVirtualCamera _virtualCamera;
        private float _baseDistance;
        private float _minDistance = 25;
        private float _baseFogStartDistance;
        private float _baseFogEndDistance;
        private float _minFogStartDistance = 15;
        private float _minFogEndDistance = 25;
        private Context _context;

        public bool SSAO => URPUtility.ssaoEnabled;
        public bool Shadows => _urpAsset.shadowDistance != 0;
        public float DrawingDistance => _serviceData.drawingDistance;
        public float RenderScale => _serviceData.renderScale;

        public void InitializeService(Context context)
        {
            _context = context;
            _playerDataService = _context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this, true);

            _urpAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
            _defaultShadowsDistance = _urpAsset.shadowDistance;
            _defaultSsaoEnabled = URPUtility.ssaoEnabled;

            _virtualCamera = context.Resolve<CinemachineVirtualCamera>();
            _baseDistance = _virtualCamera.m_Lens.FarClipPlane;
            _sdkService = _context.Resolve<SdkService>();

            context.Resolve<AddressableService>().OnSceneLoaded += () =>
            {
                _baseFogStartDistance = RenderSettings.fogStartDistance;
                _baseFogEndDistance = RenderSettings.fogEndDistance;
                ChangeDrawingDistance(_serviceData.drawingDistance, false);
            };
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData()
        {
            _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

            if (!_serviceData.settingWasChanged)
            {
                try
                {
                    var flags = _sdkService.flags;
                    
                    var drawingDistance = float.Parse(flags.DrawingDistance, CultureInfo.InvariantCulture);
                    _serviceData.drawingDistance = Math.Clamp(drawingDistance, 0f, 1f);

                    var renderScale = float.Parse(flags.RenderScale, CultureInfo.InvariantCulture);
                    _serviceData.renderScale = Math.Clamp(renderScale, 0f, 1f);

                    _serviceData.shadows = flags.Shadows == "true";
                    _serviceData.SSAO = flags.SSAO == "true";
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            ChangeSSAO(_serviceData.SSAO, false);
            ChangeShadow(_serviceData.shadows, false);
            ChangeDrawingDistance(_serviceData.drawingDistance, false);
            ChangeResolutionQuality(_serviceData.renderScale, false);
        }

        public void ChangeSSAO(bool enable, bool dirty = true)
        {
            URPUtility.ssaoEnabled = enable;
            _serviceData.SSAO = enable;
            _serviceData.settingWasChanged = true;

            if (dirty)
                _playerDataService.SetDirty(this);

            OnSsaoChange?.Invoke(SSAO);
        }

        public void ChangeShadow(bool enable, bool dirty = true)
        {
            _urpAsset.shadowDistance = enable ? _defaultShadowsDistance : 0;
            _serviceData.shadows = enable;
            _serviceData.settingWasChanged = true;

            if (dirty)
                _playerDataService.SetDirty(this);

            OnShadowChange?.Invoke(Shadows);
        }

        public void ChangeDrawingDistance(float percent, bool dirty = true)
        {
            percent = Math.Clamp(percent, 0, 1);
            _serviceData.drawingDistance = percent;
            _serviceData.settingWasChanged = true;
            OnDrawingDistanceChange?.Invoke(percent);
            // _virtualCamera.m_Lens.FarClipPlane = _minDistance + (_baseDistance - _minDistance) * percent;
            //
            // RenderSettings.fogStartDistance =
            //     _minFogStartDistance + (_baseFogStartDistance - _minFogStartDistance) * percent;
            // RenderSettings.fogEndDistance = _minFogEndDistance + (_baseFogEndDistance - _minFogEndDistance) * percent;

            if (dirty)
                _playerDataService.SetDirty(this);
        }

        public void ChangeResolutionQuality(float percent, bool dirty = true)
        {
            percent = Math.Clamp(percent, 0, 1);
            _serviceData.renderScale = percent;
            _urpAsset.renderScale = percent * .6f + .4f;
            _serviceData.settingWasChanged = true;
            OnRenderScaleChange?.Invoke(percent);

            if (dirty)
                _playerDataService.SetDirty(this);
        }

        public void EditorRestoreDefaults()
        {
            _urpAsset.shadowDistance = _defaultShadowsDistance;
            URPUtility.ssaoEnabled = _defaultSsaoEnabled;
        }

        private class ServiceData
        {
            public bool SSAO = true;
            public bool shadows = true;
            public float drawingDistance = 1f;
            public float renderScale = 1f;

            public bool settingWasChanged;
        }
    }
}
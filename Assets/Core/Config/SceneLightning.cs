#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using VInspector;

namespace Core
{
    [CreateAssetMenu(menuName = "Game Config/" + nameof(SceneLightning))]
    public class SceneLightning : ScriptableObject
    {
        [SerializeField] private bool _readCurrent;

        [SerializeField] private bool _apply;

        [field: SerializeField] public AmbientMode AmbientMode { get; private set; }
        [field: SerializeField] public Color AmbientEquatorColor { get; private set; }
        [field: SerializeField] public Color AmbientGroundColor { get; private set; }
        [field: SerializeField] public Color AmbientLight { get; private set; }
        [field: SerializeField] public Color AmbientSkyColor { get; private set; }
        [field: SerializeField] public Color FogColor { get; private set; }
        [field: SerializeField] public Color SubtractiveShadowColor { get; private set; }
        [field: SerializeField] public DefaultReflectionMode DefaultReflectionMode { get; private set; }
        [field: SerializeField] public FogMode FogMode { get; private set; }
        [field: SerializeField] public Light Sun { get; private set; }
        [field: SerializeField] public Material Skybox { get; private set; }
        [field: SerializeField] public SphericalHarmonicsL2 AmbientProbe { get; private set; }
        // [field: SerializeField] public Texture CustomReflection { get; private set; }
        [field: SerializeField] public bool Fog { get; private set; }
        [field: SerializeField] public float AmbientIntensity { get; private set; }
        [field: SerializeField] public float FlareFadeSpeed { get; private set; }
        [field: SerializeField] public float FlareStrength { get; private set; }
        [field: SerializeField] public float FogDensity { get; private set; }
        [field: SerializeField] public float FogEndDistance { get; private set; }
        [field: SerializeField] public float FogStartDistance { get; private set; }
        [field: SerializeField] public float HaloStrength { get; private set; }
        [field: SerializeField] public float ReflectionIntensity { get; private set; }
        [field: SerializeField] public int DefaultReflectionResolution { get; private set; }
        [field: SerializeField] public int ReflectionBounces { get; private set; }

        [Button,ButtonSize(22)]
        private void ReadCurrent()
        {
            AmbientEquatorColor = RenderSettings.ambientEquatorColor;
            AmbientGroundColor = RenderSettings.ambientGroundColor;
            AmbientIntensity = RenderSettings.ambientIntensity;
            AmbientLight = RenderSettings.ambientLight;
            AmbientMode = RenderSettings.ambientMode;
            AmbientProbe = RenderSettings.ambientProbe;
            AmbientSkyColor = RenderSettings.ambientSkyColor;
            // CustomReflection = RenderSettings.customReflection;
            DefaultReflectionMode = RenderSettings.defaultReflectionMode;
            DefaultReflectionResolution = RenderSettings.defaultReflectionResolution;
            FlareFadeSpeed = RenderSettings.flareFadeSpeed;
            FlareStrength = RenderSettings.flareStrength;
            Fog = RenderSettings.fog;
            FogColor = RenderSettings.fogColor;
            FogDensity = RenderSettings.fogDensity;
            FogEndDistance = RenderSettings.fogEndDistance;
            FogMode = RenderSettings.fogMode;
            FogStartDistance = RenderSettings.fogStartDistance;
            HaloStrength = RenderSettings.haloStrength;
            ReflectionBounces = RenderSettings.reflectionBounces;
            ReflectionIntensity = RenderSettings.reflectionIntensity;
            Skybox = RenderSettings.skybox;
            SubtractiveShadowColor = RenderSettings.subtractiveShadowColor;
            Sun = RenderSettings.sun;
        }

        [Button,ButtonSize(22)]
        private void ApplySaved()
        {
            RenderSettings.ambientEquatorColor = AmbientEquatorColor;
            RenderSettings.ambientGroundColor = AmbientGroundColor;
            RenderSettings.ambientIntensity = AmbientIntensity;
            RenderSettings.ambientLight = AmbientLight;
            RenderSettings.ambientMode = AmbientMode;
            RenderSettings.ambientProbe = AmbientProbe;
            RenderSettings.ambientSkyColor = AmbientSkyColor;
            // RenderSettings.customReflection = CustomReflection;
            RenderSettings.defaultReflectionMode = DefaultReflectionMode;
            RenderSettings.defaultReflectionResolution = DefaultReflectionResolution;
            RenderSettings.flareFadeSpeed = FlareFadeSpeed;
            RenderSettings.flareStrength = FlareStrength;
            RenderSettings.fog = Fog;
            RenderSettings.fogColor = FogColor;
            RenderSettings.fogDensity = FogDensity;
            RenderSettings.fogEndDistance = FogEndDistance;
            RenderSettings.fogMode = FogMode;
            RenderSettings.fogStartDistance = FogStartDistance;
            RenderSettings.haloStrength = HaloStrength;
            RenderSettings.reflectionBounces = ReflectionBounces;
            RenderSettings.reflectionIntensity = ReflectionIntensity;
            RenderSettings.skybox = Skybox;
            RenderSettings.subtractiveShadowColor = SubtractiveShadowColor;
            RenderSettings.sun = Sun;

#if UNITY_EDITOR
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
#endif
        }
    }
}
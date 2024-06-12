using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Reflection;
 
public static class URPUtility
{
    static Dictionary<System.Type, Dictionary<string, FieldInfo>> s_FieldLUT = new Dictionary<System.Type, Dictionary<string, FieldInfo>>();
    static Dictionary<System.Type, string> s_TypeNameLUT = new Dictionary<System.Type, string>();
 
    public static ScriptableRendererFeature ssao
    {
        get
        {
            const string kTypeName = "ScreenSpaceAmbientOcclusion";
            return GetRendererFeature(kTypeName);
        }
    }
 
    public static bool ssaoEnabled
    {
        get
        {
            var v = ssao;
            if (v != null)
                return v.isActive;
            return false;
        }
        set
        {
            var v = ssao;
            if (v != null)
                v.SetActive(value);
        }
    }
 
    public static float ssaoDirectLightingStrength
    {
        get => GetSettingsFloat(ssao, "m_Settings", "DirectLightingStrength");
        set => SetSettingsFloat(ssao, "m_Settings", "DirectLightingStrength", value);
    }
 
    public static float ssaoRadius
    {
        get => GetSettingsFloat(ssao, "m_Settings", "Radius");
        set => SetSettingsFloat(ssao, "m_Settings", "Radius", value);
    }
 
    public static float ssaoIntensity
    {
        get => GetSettingsFloat(ssao, "m_Settings", "Intensity");
        set => SetSettingsFloat(ssao, "m_Settings", "Intensity", value);
    }
 
    public static ScriptableRendererFeature decals
    {
        get
        {
            const string kTypeName = "DecalRendererFeature";
            return GetRendererFeature(kTypeName);
        }
    }
 
    public static float decalsMaxDrawDistance
    {
        get => GetSettingsFloat(decals, "m_Settings", "maxDrawDistance");
        set => SetSettingsFloat(decals, "m_Settings", "maxDrawDistance", value);
    }
 
    static float GetSettingsFloat(ScriptableRendererFeature feature, string settingsName, string memberName)
    {
        if (feature == null)
            return 0;
 
        var settingsMember = GetField(feature.GetType(), settingsName);
        var settings = settingsMember.GetValue(feature);
 
        var directLightingStrengthMember = GetField(settings.GetType(), memberName);
        return (float)directLightingStrengthMember.GetValue(settings);
    }
 
    static void SetSettingsFloat(ScriptableRendererFeature feature, string settingsName, string memberName, float value)
    {
        if (feature == null)
            return;
 
        var settingsMember = GetField(feature.GetType(), settingsName);
        var settings = settingsMember.GetValue(feature);
 
        var directLightingStrengthMember = GetField(settings.GetType(), memberName);
        directLightingStrengthMember.SetValue(settings, value);
    }
 
    static FieldInfo GetField(System.Type type, string fieldName)
    {
        if (!s_FieldLUT.TryGetValue(type, out var fields))
            s_FieldLUT[type] = fields = new Dictionary<string, FieldInfo>();
 
        if (!fields.TryGetValue(fieldName, out var field))
            fields[fieldName] = field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
 
        return field;
    }
 
    public static ScriptableRendererFeature GetRendererFeature(string typeName)
    {
        var asset = UniversalRenderPipeline.asset;
        var type = asset.GetType();
        var fieldInfo = GetField(type, "m_RendererDataList");
        var renderDatas = (ScriptableRendererData[])fieldInfo.GetValue(asset);
        if (renderDatas == null)
            return null;
 
        foreach (var renderData in renderDatas)
        {
            foreach (var rendererFeature in renderData.rendererFeatures)
            {
                if (rendererFeature == null)
                    continue;
 
                var featureType = rendererFeature.GetType();
                if (!s_TypeNameLUT.TryGetValue(featureType, out var name))
                    s_TypeNameLUT[featureType] = name = featureType.Name;
 
                if (name == typeName)
                    return rendererFeature;
            }
        }
 
        return null;
    }
 
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    static void RuntimeInitializeOnLoadMethod()
    {
        s_FieldLUT = new Dictionary<System.Type, Dictionary<string, FieldInfo>>();
        s_TypeNameLUT = new Dictionary<System.Type, string>();
    }
}

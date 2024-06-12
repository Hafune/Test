using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VInspector;

public class CompareParticle : AbstractCompare
{
    public ParticleSystem particle;
    public ParticleSystem testParticle;

    [Button]
    public void CompareTest() => Debug.Log(Compare(testParticle.gameObject));

    public override bool Compare(GameObject go)
    {
        var compare = go.GetComponent<ParticleSystem>();
        return Compare(particle, compare);
    }

    public static bool Compare(ParticleSystem p1, ParticleSystem p2)
    {
        if (!p2 || !p1)
            return false;

        var render = p1.GetComponent<ParticleSystemRenderer>();
        var coRender = p2.GetComponent<ParticleSystemRenderer>();

        // var main = Compare(p1.main, p2.main);
        // var emission = Compare(p1.emission, p2.emission);
        // var shape = Compare(p1.shape, p2.shape);
        // var velocityOverLifetime = Compare(p1.velocityOverLifetime, p2.velocityOverLifetime);
        // var noise = Compare(p1.noise, p2.noise);
        // var limitVelocityOverLifetime = Compare(p1.limitVelocityOverLifetime, p2.limitVelocityOverLifetime);
        // var inheritVelocity = Compare(p1.inheritVelocity, p2.inheritVelocity);
        // var lifetimeByEmitterSpeed = Compare(p1.lifetimeByEmitterSpeed, p2.lifetimeByEmitterSpeed);
        // var forceOverLifetime = Compare(p1.forceOverLifetime, p2.forceOverLifetime);
        // var colorOverLifetime = Compare(p1.colorOverLifetime, p2.colorOverLifetime);
        // var colorBySpeed = Compare(p1.colorBySpeed, p2.colorBySpeed);
        // var sizeOverLifetime = Compare(p1.sizeOverLifetime, p2.sizeOverLifetime);
        // var sizeBySpeed = Compare(p1.sizeBySpeed, p2.sizeBySpeed);
        // var rotationOverLifetime = Compare(p1.rotationOverLifetime, p2.rotationOverLifetime);
        // var rotationBySpeed = Compare(p1.rotationBySpeed, p2.rotationBySpeed);
        // var externalForces = Compare(p1.externalForces, p2.externalForces);
        // var collision = Compare(p1.collision, p2.collision);
        // var trigger = Compare(p1.trigger, p2.trigger);
        // var subEmitters = Compare(p1.subEmitters, p2.subEmitters);
        // var textureSheetAnimation = Compare(p1.textureSheetAnimation, p2.textureSheetAnimation);
        // var lights = Compare(p1.lights, p2.lights);
        // var trails = Compare(p1.trails, p2.trails);
        // var customData = Compare(p1.customData, p2.customData);
        //
        // var alignment = render.alignment == coRender.alignment;
        // var allowRoll = render.allowRoll == coRender.allowRoll;
        // var cameraVelocityScale = render.cameraVelocityScale == coRender.cameraVelocityScale;
        // var enableGPUInstancing = render.enableGPUInstancing == coRender.enableGPUInstancing;
        // var flip = render.flip == coRender.flip;
        // var freeformStretching = render.freeformStretching == coRender.freeformStretching;
        // var lengthScale = render.lengthScale == coRender.lengthScale;
        // var maskInteraction = render.maskInteraction == coRender.maskInteraction;
        // var maxParticleSize = render.maxParticleSize == coRender.maxParticleSize;
        // var mesh = render.mesh == coRender.mesh;
        // var meshDistribution = render.meshDistribution == coRender.meshDistribution;
        // var minParticleSize = render.minParticleSize == coRender.minParticleSize;
        // var normalDirection = render.normalDirection == coRender.normalDirection;
        // var pivot = render.pivot == coRender.pivot;
        // var renderMode = render.renderMode == coRender.renderMode;
        // var rotateWithStretchDirection = render.rotateWithStretchDirection == coRender.rotateWithStretchDirection;
        // var shadowBias = render.shadowBias == coRender.shadowBias;
        // var sortingFudge = render.sortingFudge == coRender.sortingFudge;
        // var sortMode = render.sortMode == coRender.sortMode;
        // var trailMaterial = render.trailMaterial == coRender.trailMaterial;
        // var velocityScale = render.velocityScale == coRender.velocityScale;
        // var allowOcclusionWhenDynamic = render.allowOcclusionWhenDynamic == coRender.allowOcclusionWhenDynamic;
        // var enabled = render.enabled == coRender.enabled;
        // var forceRenderingOff = render.forceRenderingOff == coRender.forceRenderingOff;
        // var lightmapIndex = render.lightmapIndex == coRender.lightmapIndex;
        // var lightmapScaleOffset = render.lightmapScaleOffset == coRender.lightmapScaleOffset;
        // var lightProbeUsage = render.lightProbeUsage == coRender.lightProbeUsage;
        // var motionVectorGenerationMode = render.motionVectorGenerationMode == coRender.motionVectorGenerationMode;
        // var probeAnchor = render.probeAnchor == coRender.probeAnchor;
        // var rayTracingMode = render.rayTracingMode == coRender.rayTracingMode;
        // var realtimeLightmapIndex = render.realtimeLightmapIndex == coRender.realtimeLightmapIndex;
        // var realtimeLightmapScaleOffset = render.realtimeLightmapScaleOffset == coRender.realtimeLightmapScaleOffset;
        // var receiveShadows = render.receiveShadows == coRender.receiveShadows;
        // var reflectionProbeUsage = render.reflectionProbeUsage == coRender.reflectionProbeUsage;
        // var rendererPriority = render.rendererPriority == coRender.rendererPriority;
        // var renderingLayerMask = render.renderingLayerMask == coRender.renderingLayerMask;
        // var shadowCastingMode = render.shadowCastingMode == coRender.shadowCastingMode;
        // var sharedMaterial = render.sharedMaterial == coRender.sharedMaterial;
        // var sortingLayerID = render.sortingLayerID == coRender.sortingLayerID;
        // var sortingOrder = render.sortingOrder == coRender.sortingOrder;
        // var staticShadowCaster = render.staticShadowCaster == coRender.staticShadowCaster;

        var isEqual = Compare(p1.main, p2.main) &&
                      Compare(p1.emission, p2.emission) &&
                      Compare(p1.shape, p2.shape) &&
                      Compare(p1.velocityOverLifetime, p2.velocityOverLifetime) &&
                      Compare(p1.noise, p2.noise) &&
                      Compare(p1.limitVelocityOverLifetime, p2.limitVelocityOverLifetime) &&
                      Compare(p1.inheritVelocity, p2.inheritVelocity) &&
                      Compare(p1.lifetimeByEmitterSpeed, p2.lifetimeByEmitterSpeed) &&
                      Compare(p1.forceOverLifetime, p2.forceOverLifetime) &&
                      Compare(p1.colorOverLifetime, p2.colorOverLifetime) &&
                      Compare(p1.colorBySpeed, p2.colorBySpeed) &&
                      Compare(p1.sizeOverLifetime, p2.sizeOverLifetime) &&
                      Compare(p1.sizeBySpeed, p2.sizeBySpeed) &&
                      Compare(p1.rotationOverLifetime, p2.rotationOverLifetime) &&
                      Compare(p1.rotationBySpeed, p2.rotationBySpeed) &&
                      Compare(p1.externalForces, p2.externalForces) &&
                      Compare(p1.collision, p2.collision) &&
                      Compare(p1.trigger, p2.trigger) &&
                      Compare(p1.subEmitters, p2.subEmitters) &&
                      Compare(p1.textureSheetAnimation, p2.textureSheetAnimation) &&
                      Compare(p1.lights, p2.lights) &&
                      Compare(p1.trails, p2.trails) &&
                      Compare(p1.customData, p2.customData) &&
                      render.alignment == coRender.alignment &&
                      render.allowRoll == coRender.allowRoll &&
                      render.cameraVelocityScale == coRender.cameraVelocityScale &&
                      render.enableGPUInstancing == coRender.enableGPUInstancing &&
                      render.flip == coRender.flip &&
                      render.freeformStretching == coRender.freeformStretching &&
                      render.lengthScale == coRender.lengthScale &&
                      render.maskInteraction == coRender.maskInteraction &&
                      render.maxParticleSize == coRender.maxParticleSize &&
                      render.mesh == coRender.mesh &&
                      render.meshDistribution == coRender.meshDistribution &&
                      render.minParticleSize == coRender.minParticleSize &&
                      render.normalDirection == coRender.normalDirection &&
                      render.pivot == coRender.pivot &&
                      render.renderMode == coRender.renderMode &&
                      render.rotateWithStretchDirection == coRender.rotateWithStretchDirection &&
                      render.shadowBias == coRender.shadowBias &&
                      render.sortingFudge == coRender.sortingFudge &&
                      render.sortMode == coRender.sortMode &&
                      render.trailMaterial == coRender.trailMaterial &&
                      render.velocityScale == coRender.velocityScale &&
                      render.allowOcclusionWhenDynamic == coRender.allowOcclusionWhenDynamic &&
                      render.enabled == coRender.enabled &&
                      render.forceRenderingOff == coRender.forceRenderingOff &&
                      render.lightmapIndex == coRender.lightmapIndex &&
                      render.lightmapScaleOffset == coRender.lightmapScaleOffset &&
                      render.lightProbeUsage == coRender.lightProbeUsage &&
                      render.motionVectorGenerationMode == coRender.motionVectorGenerationMode &&
                      render.probeAnchor == coRender.probeAnchor &&
                      render.rayTracingMode == coRender.rayTracingMode &&
                      render.realtimeLightmapIndex == coRender.realtimeLightmapIndex &&
                      render.realtimeLightmapScaleOffset == coRender.realtimeLightmapScaleOffset &&
                      render.receiveShadows == coRender.receiveShadows &&
                      render.reflectionProbeUsage == coRender.reflectionProbeUsage &&
                      render.rendererPriority == coRender.rendererPriority &&
                      render.renderingLayerMask == coRender.renderingLayerMask &&
                      render.shadowCastingMode == coRender.shadowCastingMode &&
                      render.sharedMaterial == coRender.sharedMaterial &&
                      render.sortingLayerID == coRender.sortingLayerID &&
                      render.sortingOrder == coRender.sortingOrder &&
                      render.staticShadowCaster == coRender.staticShadowCaster;

        return isEqual;
    }

    private static bool Compare(object o1, object o2)
    {
        var type = o1.GetType();

        var fields = type.GetFields().Where(i => !i.IsStatic);
        foreach (var field in fields)
        {
            var value1 = field.GetValue(o1);
            var value2 = field.GetValue(o2);

            if (value1 is null || value2 is null || value1.GetType().IsPrimitive || value2.GetType().IsPrimitive)
            {
                if (value1 is float float1 && value2 is float float2)
                {
                    var difference = float1 - float2;
                    if (difference < float1 * .05f || difference is < .05f or > 10000)
                        continue;
                }

                if (!Equals(value1, value2))
                    return false;
            }
            else
            {
                if (!Compare(value1, value2))
                    return false;
            }
        }

        var props = type.GetProperties().Where(i =>
            i.CanRead && i.CanWrite && i.Name != "Item" && i.Name != "materials" && i.Name != "material");

        foreach (var prop in props)
        {
            if (prop.GetCustomAttribute<ObsoleteAttribute>() is not null)
                continue;

            var value1 = prop.GetValue(o1);
            var value2 = prop.GetValue(o2);

            if (value1 is null || value2 is null || value1.GetType().IsPrimitive || value2.GetType().IsPrimitive)
            {
                if (value1 is float float1 && value2 is float float2)
                {
                    var difference = float1 - float2;
                    if (difference < float1 * .05f || difference is < .05f or > 10000)
                        continue;
                }

                if (!Equals(value1, value2))
                    return false;
            }
            else
            {
                if (value1.GetType().IsArray)
                {
                    var array1 = (Array)value1;
                    var array2 = (Array)value2;

                    if (array1.Length != array2.Length)
                        return false;

                    for (int i = 0, iMax = array1.Length; i < iMax; i++)
                        if (!Compare(array1.GetValue(i), array2.GetValue(i)))
                            return false;
                }
                else if (!Compare(value1, value2))
                    return false;
            }
        }

        return true;
    }
}
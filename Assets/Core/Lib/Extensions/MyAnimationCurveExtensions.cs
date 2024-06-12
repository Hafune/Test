using UnityEngine;

namespace Lib
{
    public static class MyAnimationCurveExtensions
    {
        public static AnimationCurve MakeSwapTimeAndValue(this AnimationCurve curve)
        {
            var keys = curve.keys;
            var newKeys = new Keyframe[keys.Length];

            for (int i = 0, iMax = keys.Length - 1; i < iMax; i++)
            {
                var frame0 = keys[i];
                var f0 = newKeys[i];
                f0.time = frame0.value;
                f0.value = frame0.time;

                var frame1 = keys[i + 1];
                var f1 = newKeys[i + 1];
                f1.time = frame1.value;
                f1.value = frame1.time;
                
                f0.outTangent = (f1.value - f0.value) / (f1.time - f0.time);
                f1.inTangent = f0.outTangent;

                newKeys[i] = f0;
                newKeys[i + 1] = f1;
            }

            return new AnimationCurve(newKeys);
        }
    }
}
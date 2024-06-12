using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace Lib
{
    public static class MyExtensions
    {
        public static void RepeatTimes(this int count, Action callback)
        {
            for (int i = 0; i < count; i++)
                callback.Invoke();
        }

        public static int Sign(this int value) => Math.Sign(value);

        public static int Sign(this float value) => Math.Sign(value);
        public static int Abs(this int value) => Math.Abs(value);
        public static float Abs(this float value) => Math.Abs(value);

        public static float RoundToDecimal(this float value, int accuracy)
        {
            var acc = (float)Math.Pow(10, accuracy);
            return (int)(value * acc) / acc;
        }

        public static bool ContainsPoint(this BoxCollider box, Vector3 point, Vector3? sizeMultiply = null)
        {
            var m = sizeMultiply ?? new Vector3(1, 1, 1);
            point = box.transform.InverseTransformPoint(point) - box.center;
            float halfX = box.size.x * 0.5f * m.x;
            float halfY = box.size.y * 0.5f * m.y;
            float halfZ = box.size.z * 0.5f * m.z;
            return point.x < halfX && point.x > -halfX &&
                   point.y < halfY && point.y > -halfY &&
                   point.z < halfZ && point.z > -halfZ;
        }

        public static bool Raycast(this Camera camera, Vector3 position, out RaycastHit info) =>
            Physics.Raycast(camera.ScreenPointToRay(position), out info);

        public static AnimationClip GetAnimationClipByName(this Animator animator, string name) =>
            animator.runtimeAnimatorController.animationClips.First(clip => clip.name == name);

        public static AnimationClip GetAnimationClipByName(this Animator animator, int name) =>
            animator.runtimeAnimatorController.animationClips.First(clip => Animator.StringToHash(clip.name) == name);

        public static int NameToLayer(this Layers layer) => LayerMask.NameToLayer(layer.ToString());

        public static Vector3 LineTo(this Transform from, Transform to) => to.position - from.position;

        public static Vector3 LineTo(this Component from, Component to) =>
            to.transform.position - from.transform.position;

        public static Vector3 CenterBetween(this Transform pointA, Transform pointB) =>
            (pointB.position + pointA.position) / 2f;

        public static Vector3 CenterBetween(this Component pointA, Component pointB) =>
            (pointB.transform.position + pointA.transform.position) / 2f;

        public static void CopyPosition(this Transform from, Transform to, float? x = null, float? y = null,
            float? z = null)
        {
            var position = to.position;
            from.position =
                new Vector3(
                    x ?? position.x,
                    y ?? position.y,
                    z ?? position.z
                );
        }

        public static void AssertIsNullOrEmpty(this string value)
        {
            Assert.IsNotNull(value);
            Assert.AreNotEqual(value, "");
        }

        public static void ExceptionIfInvocationsMoreThanOne(this Action action)
        {
            if (action.GetInvocationList().Length > 1)
                throw new Exception("Больше одной подписки");
        }

        public static void ExceptionIfInvocationsMoreThanOne<T>(this Action<T> action)
        {
            if (action.GetInvocationList().Length > 1)
                throw new Exception("Больше одной подписки");
        }
    }
}
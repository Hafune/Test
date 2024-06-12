using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Random = System.Random;

namespace Lib
{
    public static class MyFunctions
    {
        private static Random random = new();
        private static Assembly[] _assemblies;

        public static void ForEachDimensions(int[] dimensions, Action<int[]> callback)
        {
            var args = new int[dimensions.Length];
            RecursiveCallback(dimensions, args, 0, callback);
        }

        private static void RecursiveCallback(IReadOnlyList<int> dimensions, int[] array, int index,
            Action<int[]> callback)
        {
            for (int i = 0; i < dimensions[index]; i++)
            {
                array[index] = i;

                if (index < array.Length - 1)
                    RecursiveCallback(dimensions, array, index + 1, callback);
                else
                    callback.Invoke(array);
            }
        }

        public static void RepeatTimesIndexed(int t0, int t1, int t2, Action<int, int, int> callback)
        {
            for (int _t0 = 0; _t0 < t0; _t0++)
            for (int _t1 = 0; _t1 < t1; _t1++)
            for (int _t2 = 0; _t2 < t2; _t2++)
                callback.Invoke(_t0, _t1, _t2);
        }

        public static Vector3 AddHorizontalVelocityToDirection(Rigidbody rigidbody, Vector3 desiredVelocity)
        {
            var velocityInDirection = rigidbody.velocity;
            var gravity = velocityInDirection.y;
            velocityInDirection.y = 0;

            if (Vector3.Dot(velocityInDirection, desiredVelocity) < 0)
            {
                rigidbody.velocity += desiredVelocity;
            }
            else if (velocityInDirection.sqrMagnitude < desiredVelocity.sqrMagnitude)
            {
                velocityInDirection += desiredVelocity;
                rigidbody.velocity = velocityInDirection.normalized * desiredVelocity.magnitude + Vector3.up * gravity;
            }
            else
            {
                var currentSpeed = velocityInDirection.magnitude;
                velocityInDirection = (velocityInDirection + desiredVelocity).normalized * currentSpeed;
                rigidbody.velocity = velocityInDirection + Vector3.up * gravity;
            }

            return velocityInDirection;
        }

        public static void ClearConsole()
        {
#if UNITY_EDITOR
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method!.Invoke(new object(), null);
#endif
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static int ExtractLayerMask(LayerMask layer)
        {
            int layerMask = 0;

            for (int i = 0; i < 32; i++)
                if (((1 << i) & layer.value) != 0)
                    layerMask |= Physics2D.GetLayerCollisionMask(i);

            return layerMask;
        }

        public static Type[] GetAssignableTypes<T>(this AppDomain appDomain, bool findGenerics = false) 
        {
            var assemblies = _assemblies ??= appDomain.GetAssemblies();

            return assemblies
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(type =>
                        typeof(T).IsAssignableFrom(type) && !type.IsAbstract && type.IsGenericType == findGenerics))
                .ToArray();
        }
#if UNITY_EDITOR
        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            var type = original.GetType();
            var dst = destination.AddComponent(type) as T;

            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic) continue;
                field.SetValue(dst, field.GetValue(original));
            }

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (!prop.CanWrite ||
                    prop.GetCustomAttribute<ObsoleteAttribute>() is not null ||
                    prop.Name == "name") continue;
                prop.SetValue(dst, prop.GetValue(original, null), null);
            }

            return dst;
        }
#endif
    }
}
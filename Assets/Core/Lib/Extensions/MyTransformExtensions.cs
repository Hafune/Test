using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lib
{
    public static class MyTransformExtensions
    {
        public static void ForEachSelfChildren<T>(this Transform transform, Action<T> callback)
        {
            foreach (Transform child in transform)
            foreach (var script in child.GetComponents<T>())
                callback.Invoke(script);
        }

        public static void ForEachSelfChildren<T>(this Transform transform, Action<T> callback,
            bool includeInactive)
        {
            foreach (Transform child in transform)
                if (child.gameObject.activeSelf || includeInactive)
                    foreach (var script in child.GetComponents<T>())
                        callback.Invoke(script);
        }

        public static Transform[] GetSelfChildrenTransforms(this Transform transform)
        {
            var list = new List<Transform>();
            foreach (Transform child in transform)
                list.Add(child);

            return list.ToArray();
        }

        public static T[] GetSelfChildrenComponents<T>(this Transform transform, bool includeInactive)
        {
            var list = new List<T>();
            foreach (Transform child in transform)
                if (child.gameObject.activeSelf || includeInactive)
                    list.AddRange(child.gameObject.GetComponents<T>());

            return list.ToArray();
        }

        public static T GetSelfChildrenComponent<T>(this Transform transform) where T : class
        {
            foreach (Transform child in transform)
                if (child.gameObject.TryGetComponent<T>(out var value))
                    return value;

            return null;
        }

        public static T[] GetSelfChildrenComponents<T>(this Transform transform)
        {
            var list = new List<T>();
            foreach (Transform child in transform)
                list.AddRange(child.gameObject.GetComponents<T>());

            return list.ToArray();
        }

        public static Transform FindRecursiveFirst(this Transform parent, string childName, bool ignoreCase = false)
        {
            foreach (Transform child in parent)
            {
                switch (ignoreCase)
                {
                    case false when child.name.Trim() == childName:
                        return child;
                    case true when string.Equals(child.name.Trim(), childName,
                        StringComparison.CurrentCultureIgnoreCase):
                        return child;
                }

                var found = FindRecursiveFirst(child, childName, ignoreCase);

                if (found != null)
                    return found;
            }

            return null;
        }

        public static Transform FindRecursiveFirstStartsWith(this Transform parent, string childName)
        {
            foreach (Transform child in parent)
            {
                if (child.name.StartsWith(childName, true, CultureInfo.CurrentCulture))
                    return child;

                var found = FindRecursiveFirstStartsWith(child, childName);

                if (found != null)
                    return found;
            }

            return null;
        }

        public static List<Transform> FindRecursiveAllStartsWith(this Transform parent, string childName,
            bool ignoreCase = false)
        {
            var list = new List<Transform>();
            FindRecursiveAllStartsWith(parent, childName, list, ignoreCase);

            return list;
        }

        private static void FindRecursiveAllStartsWith(this Transform parent, string childName, List<Transform> list,
            bool ignoreCase)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Trim().StartsWith(childName, ignoreCase, CultureInfo.CurrentCulture))
                    list.Add(child);

                FindRecursiveAllStartsWith(child, childName, list, ignoreCase);
            }
        }

        public static List<Transform> FindRecursiveAll(this Transform parent, string childName, bool ignoreCase = false)
        {
            var list = new List<Transform>();
            FindRecursiveAll(parent, childName, list, ignoreCase);

            return list;
        }

        private static void FindRecursiveAll(this Transform parent, string childName, List<Transform> list,
            bool ignoreCase)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Trim().Equals(childName,
                        ignoreCase
                            ? StringComparison.CurrentCultureIgnoreCase
                            : StringComparison.CurrentCulture))
                    list.Add(child);

                FindRecursiveAll(child, childName, list, ignoreCase);
            }
        }

        public static void DestroyChildren(this Transform transform, bool allowDestroyingAssets = false)
        {
            while (transform.childCount > 0)
            {
                if (Application.isPlaying)
                    Object.Destroy(transform.GetChild(0).gameObject);
                else
                    Object.DestroyImmediate(transform.GetChild(0).gameObject, allowDestroyingAssets);
            }
        }
    }
}
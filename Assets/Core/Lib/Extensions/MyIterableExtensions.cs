using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lib
{
    public static class MyIterableExtensions
    {
        public static T[,] InitializeWIth<T>(this T[,] array, Func<T> initFunc)
        {
            for (int t0 = 0; t0 < array.GetLength(0); t0++)
            for (int t1 = 0; t1 < array.GetLength(1); t1++)
                array[t0, t1] = initFunc.Invoke();

            return array;
        }

        public static T[,,] InitializeWIth<T>(this T[,,] array, Func<T> initFunc)
        {
            for (int t0 = 0; t0 < array.GetLength(0); t0++)
            for (int t1 = 0; t1 < array.GetLength(1); t1++)
            for (int t2 = 0; t2 < array.GetLength(2); t2++)
                array[t0, t1, t2] = initFunc.Invoke();

            return array;
        }

        public static T MinBy<T>(this List<T> list, Func<T, float> compare)
        {
            return list.OrderBy(compare).First();
        }

        public static T MaxBy<T>(this T[] list, Func<T, float> compare)
        {
            return list.OrderByDescending(compare).First();
        }

        public static T MaxByOrDefault<T>(this List<T> list, Func<T, float> compare) =>
            list.OrderByDescending(compare).FirstOrDefault();

        private static T Max<T>(List<T> list, Func<T, float> compare, T value) =>
            list.OrderByDescending(compare).First();

        public static int SumBy<T>(this IList<T> list, Func<T, int> callback) => list.Sum(callback.Invoke);
        public static float SumBy<T>(this IList<T> list, Func<T, float> callback) => list.Sum(callback.Invoke);
        public static int SumBy<T>(this HashSet<T> set, Func<T, int> callback) => set.Sum(callback.Invoke);
        public static float SumBy<T>(this HashSet<T> set, Func<T, float> callback) => set.Sum(callback.Invoke);

        public static Vector2 SumBy<T>(this Queue<T> list, Func<T, Vector2> callback)
        {
            var agg = Vector2.zero;

            foreach (var v in list)
                agg += callback.Invoke(v);

            return agg;
        }

        public static void Deconstruct<T>(this T[] array, out T t0) => t0 = array[0];

        public static void Deconstruct<T>(this T[] array, out T t0, out T t1)
        {
            t0 = array[0];
            t1 = array[1];
        }

        public static void Deconstruct<T>(this T[] array, out T t0, out T t1, out T t2)
        {
            t0 = array[0];
            t1 = array[1];
            t2 = array[2];
        }

        public static void ForEachIndexed<T>(this List<T> list, Action<T, int> callback)
        {
            for (int i = 0, count = list.Count; i < count; i++)
                callback.Invoke(list[i], i);
        }

        public static List<T> Initialize<T>(this List<T> list, int size, Func<T> initFoo)
        {
            for (int i = 0; i < size; i++)
                list.Add(initFoo());

            return list;
        }

        public static bool IsEmpty<T>(this List<T> list) => list.Count == 0;

        public static bool IsNotEmpty<T>(this List<T> list) => list.Count != 0;

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> callback)
        {
            foreach (var item in enumerable)
                callback.Invoke(item);
        }

        public static void ForEachIndexed<T>(this T[] array, Action<T, int> callback)
        {
            for (int i = 0, count = array.Length; i < count; i++)
                callback.Invoke(array[i], i);
        }

        public static T Random<T>(this IList<T> list) => list[UnityEngine.Random.Range(0, list.Count)];
        public static int RandomIndex<T>(this IList<T> list) => UnityEngine.Random.Range(0, list.Count);

        public static T Pop<T>(this List<T> list)
        {
            int index = list.Count - 1;
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static T CircularNext<T>(this List<T> collection, T item) =>
            Circular(collection, collection.FindIndex(l => l.Equals(item)) + 1);

        public static T CircularPrevious<T>(this List<T> collection, T item) =>
            Circular(collection, collection.FindIndex(l => l.Equals(item)) - 1);

        public static int CircularNextIndex<T>(this List<T> collection, T item) =>
            CircularIndex(collection, collection.FindIndex(l => l.Equals(item)) + 1);

        public static int CircularPreviousIndex<T>(this List<T> collection, T item) =>
            CircularIndex(collection, collection.FindIndex(l => l.Equals(item)) - 1);

        public static T Circular<T>(this List<T> collection, int index) => collection[CircularIndex(collection, index)];

        public static int CircularIndex<T>(this List<T> collection, int index)
        {
            var _index = index % collection.Count;

            if (_index < 0)
                _index = collection.Count + index;

            return _index;
        }
        
        public static void SortBy<T, T2>(this List<T> list, Func<T, T2> keySelector) where T2 : System.IComparable => list.Sort((q, w) => keySelector(q).CompareTo(keySelector(w)));
    }
}
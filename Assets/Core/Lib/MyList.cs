using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.Lib
{
    [Serializable]
    public class MyList<T>
    {
        public T[] Items { get; private set; }

        public int Count { get; private set; }

        public MyList()
        {
            Items = new T[4]; // Initial capacity
            Count = 0;
        }

        public void Add(T item)
        {
            if (Count == Items.Length)
                ResizeArray();

            Items[Count++] = item;
        }

        public bool Remove(T item)
        {
            int index = Array.IndexOf(Items, item, 0, Count);
            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
                Items[i] = Items[i + 1];

            Count--;
        }

        private void ResizeArray()
        {
            var newItems = new T[Items.Length * 2];
            Array.Copy(Items, newItems, Items.Length);
            Items = newItems;
        }

        public void Clear()
        {
            if (Count <= 0)
                return;

            Array.Clear(Items, 0, Count);
            Count = 0;
        }

        public void RemoveWhere(T value)
        {
            while (Remove(value))
            {
            }
        }

        public T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (Count <= index)
                    LogError(index);

                return Items[index];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (Count <= index)
                    LogError(index);

                Items[index] = value;
            }
        }

        private void LogError(int index) =>
            Debug.LogError($"MyArray: Index out of Bounds {index}, total length {Count}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator() => new(this);

        public ref struct Enumerator
        {
            private readonly int _count;
            private readonly T[] _data;
            private int _idx;

            public Enumerator(MyList<T> data)
            {
                _count = data.Count;
                _data = data.Items;
                _idx = -1;
            }

            public T Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _data[_idx];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() => ++_idx < _count;
        }
    }
}
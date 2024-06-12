using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Core.Lib
{
    [DebuggerDisplay("Length = {Length}")]
    [SuppressMessage("ReSharper", "InvertIf")]
    public class Glossary<TValue>
    {
        private static TValue _defaultValue;
        private const int StartOfFreeList = -3;

        private int[] _buckets;
        private Entry[] _entries;
        private int _length;
        private int _freeCount;
        private int _freeList;

        public Glossary() : this(0)
        {
            
        }

        public Glossary(int capacity)
        {
            capacity = GlossaryHelper.GetPrime(capacity);

            _buckets = new int[capacity];
            _entries = new Entry[capacity];
            _freeCount = 0;
            _freeList = 0;
            _length = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TValue Add(int key)
        {
            if (_buckets.Length == 0) Resize();

            var keyHash = (uint)key;
            ref var bucket = ref GetBucket(keyHash);
            var entries = _entries;
            var i = bucket - 1;

            while ((uint)i < (uint)entries.Length)
            {
                ref readonly var existsEntry = ref entries[i];
                if (existsEntry.Key == key) GlossaryHelper.KeyAlreadyExists(key);

                i = existsEntry.Next;
            }

            int index;
            if (_freeCount > 0)
            {
                index = _freeList;
                _freeList = StartOfFreeList - entries[index].Next;
                _freeCount--;
            }
            else
            {
                index = _length;
                if (index == entries.Length)
                {
                    Resize();
                    bucket = ref GetBucket(keyHash);
                    entries = _entries;
                }

                _length++;
            }

            ref var entry = ref entries[index];

            entry.Key = key;
            entry.Next = bucket - 1;

            bucket = index + 1;

            return ref entry.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(int key, in TValue value)
        {
            ref var entryValue = ref Add(key);
            entryValue = value;
        }

        public void Clear()
        {
            if (_length == 0) return;

            Array.Clear(_buckets, 0, _buckets.Length);
            Array.Clear(_entries, 0, _entries.Length);

            _freeCount = 0;
            _freeList = 0;
            _length = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(int key)
        {
            if (_length > 0)
            {
                var i = _buckets[(uint)key % (uint)_buckets.Length] - 1;
                var entries = _entries;
                while ((uint)i < (uint)entries.Length)
                {
                    ref readonly var entry = ref entries[i];
                    if (entry.Key == key) return true;
                    i = entry.Next;
                }
            }

            return false;
        }

        public bool ContainsValue(in TValue value, IEqualityComparer<TValue> comparer = null)
        {
            comparer ??= EqualityComparer<TValue>.Default;
            foreach (var entry in _entries.AsSpan(0, _length))
            {
                if (entry.Next >= -1 && comparer.Equals(entry.Value, value))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator() => new(_entries, _length);

        public ref TValue GetValue(int key)
        {
            if (_length > 0)
            {
                var i = _buckets[(uint)key % (uint)_buckets.Length] - 1;
                var entries = _entries;
                while ((uint)i < (uint)entries.Length)
                {
                    ref var entry = ref entries[i];
                    if (entry.Key == key) return ref entry.Value;
                    i = entry.Next;
                }
            }

            GlossaryHelper.KeyNotFound(key);
            return ref _defaultValue;
        }

        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _length - _freeCount;
        }

        public bool Remove(int key, bool clear = false)
        {
            if (_length == 0) return false;

            ref var bucket = ref GetBucket((uint)key);
            var entries = _entries;
            var i = bucket - 1;
            var last = -1;

            while (i >= 0)
            {
                ref var entry = ref entries[i];
                if (entry.Key == key)
                {
                    if (clear) entry.Value = default;

                    if (last < 0) bucket = entry.Next + 1;
                    else entries[last].Next = entry.Next;

                    entry.Next = StartOfFreeList - _freeList;

                    _freeCount++;
                    _freeList = i;
                    return true;
                }

                last = i;
                i = entry.Next;
            }

            return false;
        }

        public bool Remove(int key, out TValue value)
        {
            if (_length == 0)
            {
                value = default;
                return false;
            }

            ref var bucket = ref GetBucket((uint)key);
            var entries = _entries;
            var i = bucket - 1;
            var last = -1;

            while (i >= 0)
            {
                ref var entry = ref entries[i];
                if (entry.Key == key)
                {
                    value = entry.Value; // copy
                    entry.Value = default;

                    if (last < 0) bucket = entry.Next + 1;
                    else entries[last].Next = entry.Next;

                    entry.Next = StartOfFreeList - _freeList;

                    _freeCount++;
                    _freeList = i;
                    return true;
                }

                last = i;
                i = entry.Next;
            }

            value = default;
            return false;
        }

        public void TrimExcess()
        {
            if (_length == 0)
            {
                _buckets = Array.Empty<int>();
                _entries = Array.Empty<Entry>();
                _freeCount = 0;
                _freeList = -1;

                return;
            }

            var oldEntries = _entries;
            var newSize = _length;

            var buckets = new int[newSize];
            var entries = new Entry[newSize];
            var count = 0;
            for (var i = 0; i < newSize; i++)
            {
                if (oldEntries[i].Next < -1) continue;

                ref var entry = ref entries[count];
                entry = oldEntries[i];
                var bucket = (uint)oldEntries[i].Key % (uint)newSize;
                entry.Next = buckets[bucket] - 1;
                buckets[bucket] = count + 1;
                count++;
            }

            _buckets = buckets;
            _entries = entries;
            _freeCount = 0;
            _freeList = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(int key, out TValue value)
        {
            if (_length > 0)
            {
                var i = _buckets[(uint)key % (uint)_buckets.Length] - 1;
                var length = (uint)_entries.Length;
                while ((uint)i < length)
                {
                    ref readonly var entry = ref _entries[i];
                    if (entry.Key == key)
                    {
                        value = entry.Value;
                        return true;
                    }

                    i = entry.Next;
                }
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ref int GetBucket(uint keyHash) => ref _buckets[keyHash % (uint)_buckets.Length];

        private void Resize()
        {
            var length = _length;
            var newSize = GlossaryHelper.GetPrime(length == 0 ? 4 : length << 1);

            var buckets = new int[newSize];
            var entries = new Entry[newSize];

            Array.Copy(_entries, entries, length);

            for (var i = 0; i < length; i++)
            {
                ref var entry = ref entries[i];

                if (entry.Next < -1) continue;

                ref var bucket = ref buckets[(uint)entry.Key % (uint)buckets.Length];
                entry.Next = bucket - 1;
                bucket = i + 1;
            }

            _buckets = buckets;
            _entries = entries;
        }

        public struct Entry
        {
            internal int Next;
            public int Key;
            public TValue Value;
        }

        public ref struct Enumerator
        {
            private int _index;
            private readonly int _length;
            private readonly Entry[] _entries;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator(Entry[] entries, int length)
            {
                _index = -1;
                _length = length;
                _entries = entries;
            }

            public bool MoveNext()
            {
                var index = _index + 1;
                var length = _length;
                while ((uint)index < (uint)length)
                {
                    ref readonly var entry = ref _entries[index];
                    if (entry.Next >= -1)
                    {
                        _index = index;
                        return true;
                    }

                    index++;
                }

                _index = _length;
                return false;
            }

            public readonly ref Entry Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => ref _entries[_index];
            }
        }
    }

    public static class GlossaryHelper
    {
        public static int GetPrime(int min)
        {
            foreach (var prime in primes)
            {
                if (prime >= min) return prime;
            }

            for (var candidate = min | 1; candidate < int.MaxValue; candidate += 2)
            {
                if (IsPrime(candidate) && (candidate - 1) % 101 != 0) return candidate;
            }

            return min;
        }

        public static void KeyAlreadyExists(int key)
        {
            throw new Exception($"Key {key} already exists");
        }

        public static void KeyNotFound(int key)
        {
            throw new Exception($"Key {key} isn't found");
        }

        private static bool IsPrime(int candidate)
        {
            if ((candidate & 1) == 0) return candidate == 2;
            var num = (int)Math.Sqrt((double)candidate);
            for (var index = 3; index <= num; index += 2)
            {
                if (candidate % index == 0)
                    return false;
            }

            return true;
        }

        private static readonly int[] primes = new int[72]
        {
            3,
            7,
            11,
            17,
            23,
            29,
            37,
            47,
            59,
            71,
            89,
            107,
            131,
            163,
            197,
            239,
            293,
            353,
            431,
            521,
            631,
            761,
            919,
            1103,
            1327,
            1597,
            1931,
            2333,
            2801,
            3371,
            4049,
            4861,
            5839,
            7013,
            8419,
            10103,
            12143,
            14591,
            17519,
            21023,
            25229,
            30293,
            36353,
            43627,
            52361,
            62851,
            75431,
            90523,
            108631,
            130363,
            156437,
            187751,
            225307,
            270371,
            324449,
            389357,
            467237,
            560689,
            672827,
            807403,
            968897,
            1162687,
            1395263,
            1674319,
            2009191,
            2411033,
            2893249,
            3471899,
            4166287,
            4999559,
            5999471,
            7199369
        };
    }
}
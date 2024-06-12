using System.Runtime.CompilerServices;
using Core.Lib;
using UnityEngine;

namespace Core.Components
{
    public sealed class DamageData
    {
        private readonly MyList<float> _damages = new();
        private readonly MyList<Vector3> _triggerPoints = new();
        private readonly MyList<Vector3> _ownerPositions = new();
        private readonly MyList<int> _owners = new();
        private readonly MyList<EffectWithText> _textEffectPrefabs = new();

        public void Add(
            float damage,
            Vector3 triggerPoint,
            Vector3 ownerPosition,
            int owner,
            EffectWithText effect)
        {
            _damages.Add(damage);
            _triggerPoints.Add(triggerPoint);
            _ownerPositions.Add(ownerPosition);
            _owners.Add(owner);
            _textEffectPrefabs.Add(effect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (float damage, Vector3 point, int owner, EffectWithText textEffectPrefab)
            Get(int index) =>
            (_damages.Items[index], _triggerPoints.Items[index], _owners.Items[index], _textEffectPrefabs.Items[index]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetOwnerPositionAt(int index) => _ownerPositions.Items[index];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetPointAt(int index) => _triggerPoints.Items[index];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetDamageAt(int index) => _damages.Items[index];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDamageAt(int index, float value) => _damages.Items[index] = value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateDamageValue(int index, float value) => _damages.Items[index] = value;

        public int GetDamagesCount() => _damages.Count;

        public void AutoReset()
        {
            _damages.Clear();
            _triggerPoints.Clear();
            _ownerPositions.Clear();
            _owners.Clear();
            _textEffectPrefabs.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public ref struct Enumerator
        {
            readonly int _count;
            int _idx;

            public Enumerator(DamageData data)
            {
                _count = data.GetDamagesCount();
                _idx = -1;
            }

            public int Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _idx;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() => ++_idx < _count;
        }
    }
}
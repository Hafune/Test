using Lib;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core
{
    [DisallowMultipleComponent]
    public class SpatialHashItem : MonoConstruct, ISpatialHashItem
    {
        [field: SerializeField] public float Radius { get; set; }
        [field: SerializeField] public ConvertToEntity ConvertToEntity { get; private set; }
        private SpatialHashingMono _hashingMono;

        public Vector2Int LastCell { get; set; }

        private void OnValidate() =>
            ConvertToEntity = ConvertToEntity ? ConvertToEntity : GetComponent<ConvertToEntity>();

        protected override void Construct(Context context) =>
            _hashingMono = context.Resolve<SpatialHashingMono>();

        // private void FixedUpdate() => _hashingMono.Root.Update(this);

        private void OnDisable() => _hashingMono.Root.Remove(this);
    }
}
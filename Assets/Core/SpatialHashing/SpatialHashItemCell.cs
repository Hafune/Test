using Lib;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core
{
    [DisallowMultipleComponent]
    public class SpatialHashItemCell : MonoConstruct, ISpatialHashItem
    {
        [field: SerializeField] public float Radius { get; set; }
        [field: SerializeField] public ConvertToEntity ConvertToEntity { get; private set; }
        private SpatialHashingCellMono _hashingMono;

        public Vector2Int LastCell { get; set; }

        private void OnValidate() =>
            ConvertToEntity = ConvertToEntity ? ConvertToEntity : GetComponent<ConvertToEntity>();

        protected override void Construct(Context context) =>
            _hashingMono = context.Resolve<SpatialHashingCellMono>();

        private void OnEnable() => _hashingMono.Root.Add(this);
        
        private void OnDisable() => _hashingMono.Root.Remove(this);
        
        // private void FixedUpdate() => _hashingMono.Root.Update(this);
    }
}
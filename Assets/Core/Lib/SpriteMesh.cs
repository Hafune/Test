using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class SpriteMesh : MonoBehaviour
    {
        private static readonly Glossary<Material> MaterialCache = new();
        
        [SerializeField] private Material _materialTemplate;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private bool _refresh;

        public Sprite sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;
                UpdateMesh();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_refresh)
                return;
            
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;

            _refresh = false;
            MaterialCache.Clear();
            _renderer ??= GetComponent<MeshRenderer>();
            UpdateMesh();
        }
#endif

        private void UpdateMesh()
        {
            var texture = _sprite ? _sprite.texture : null;
            
            if (texture is null)
            {
                _renderer.material = null;
                return;
            }

            int id = texture.GetInstanceID();
            
            if (MaterialCache.TryGetValue(id, out var material))
            {
                _renderer.material = material;
                return;
            }

            var newMaterial = Instantiate(_materialTemplate);
            newMaterial.mainTexture = texture;

            var bottomLeft = new Vector2(float.MaxValue, float.MaxValue);
            var topRight = new Vector2(float.MinValue, float.MinValue);

            foreach (var v in _sprite.uv)
            {
                bottomLeft.x = Math.Min(v.x, bottomLeft.x);
                bottomLeft.y = Math.Min(v.y, bottomLeft.y);

                topRight.x = Math.Max(v.x, topRight.x);
                topRight.y = Math.Max(v.y, topRight.y);
            }

            newMaterial.mainTextureOffset = bottomLeft;
            newMaterial.mainTextureScale = topRight - bottomLeft;
            _renderer.material = newMaterial;
            MaterialCache.Add(id, newMaterial);
        }
    }
}
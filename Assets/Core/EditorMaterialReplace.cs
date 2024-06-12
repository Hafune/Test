using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.U2D;
using VInspector;

public class EditorMaterialReplace : MonoConstruct
{
    private static readonly int BaseMapId = Shader.PropertyToID("_BaseMap");
    private static readonly int MetallicGlossMapId = Shader.PropertyToID("_MetallicGlossMap");
    private static readonly int BumpMapId = Shader.PropertyToID("_BumpMap");
    private static readonly Glossary<Sprite[]> SpritesCache = new();
    private static readonly Glossary<Material> MaterialCache = new();
    private static readonly HashSet<int> IgnoredMaterialCache = new();
    private static readonly Dictionary<Vector2, Material> MaterialsByOffset = new();

    [SerializeField] private bool _replaceWhenMapCreated;
    [SerializeField] private Material _materialTemplate;
    [SerializeField] private SpriteAtlas _baseAtlas;
    [SerializeField] private SpriteAtlas _metallicAtlas;
    [SerializeField] private SpriteAtlas _normalAtlas;
#if UNITY_EDITOR
    [SerializeField] private CreateAssetMaterial _createAssetMaterial;
    [Button]
    private void MakeMaterialAssets()
    {
        var set = new HashSet<Material>();
        foreach (var entry in MaterialCache)
            set.Add(entry.Value);

        foreach (var material in set)
            _createAssetMaterial.Create(material);
    }
#endif

    private Context _context;
    private MapConstructorService _mapConstructorService;

    protected override void Construct(Context context) => _context = context;

    private void Awake()
    {
#if UNITY_EDITOR
        if (_context is null)
        {
            Debug.LogWarning($"{nameof(EditorMaterialReplace)} режим без контекста");
            return;
        }
#endif

        _mapConstructorService = _context.Resolve<MapConstructorService>();

        if (_replaceWhenMapCreated)
            _mapConstructorService.OnMapCreated += UpdateMaterials;
    }

    private void OnDestroy() => _mapConstructorService.OnMapCreated -= UpdateMaterials;

    private void Start()
    {
        MakeSpritesMapCache(_baseAtlas);
        MakeSpritesMapCache(_metallicAtlas);
        MakeSpritesMapCache(_normalAtlas);
    }

    private void MakeSpritesMapCache(SpriteAtlas atlas)
    {
        var id = atlas.GetInstanceID();

        if (SpritesCache.ContainsKey(id))
            return;

        var sprites = new Sprite[atlas.spriteCount];
        atlas.GetSprites(sprites);

        foreach (var sprite in sprites)
            sprite.name = sprite.name.Replace("(Clone)", "");

        SpritesCache.Add(id, sprites);
    }

    [Button]
    private void UpdateMaterials()
    {
        foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>(true))
        {
            bool _wasChanged = false;
            var materials = meshRenderer.materials;
            for (int i = 0, iMax = materials.Length; i < iMax; i++)
            {
                materials[i] = MakeMaterial(materials[i]);

                if (materials[i] is null)
                    continue;

                _wasChanged = true;
            }

            if (!_wasChanged)
                continue;

            meshRenderer.materials = materials;
        }
    }

    private Material MakeMaterial(Material currentMaterial)
    {
        var id = Animator.StringToHash(currentMaterial.name);
        Debug.Log(currentMaterial.name);
        if (IgnoredMaterialCache.Contains(id))
            return null;

        if (MaterialCache.TryGetValue(id, out var material))
            return material;

        var textureName = currentMaterial.HasTexture(BaseMapId)
            ? currentMaterial.GetTexture(BaseMapId)?.name
            : null;

        if (textureName is null)
        {
            IgnoredMaterialCache.Add(id);
            return null;
        }

        var sprite = SpritesCache
            .GetValue(_baseAtlas.GetInstanceID())
            .FirstOrDefault(i => i.name == textureName);

        if (sprite is null)
        {
            IgnoredMaterialCache.Add(id);
            return null;
        }

        var newMaterial = Instantiate(_materialTemplate);

        newMaterial.SetTexture(BaseMapId, sprite!.texture);
        newMaterial.name = currentMaterial.name.Replace("Instance", "").Replace(" ()", "").Trim() + "_atlas";
        var offset = RefreshOffsetAndScale(sprite.uv, newMaterial, BaseMapId);

        if (MaterialsByOffset.TryGetValue(offset, out var existMaterial))
        {
            newMaterial = existMaterial;
        }
        else
        {
            MaterialsByOffset.Add(offset, newMaterial);
        }

        RefreshTexture(_metallicAtlas, currentMaterial, newMaterial, MetallicGlossMapId);
        RefreshTexture(_normalAtlas, currentMaterial, newMaterial, BumpMapId);

        MaterialCache.Add(id, newMaterial);
        return newMaterial;
    }

    private void RefreshTexture(
        SpriteAtlas atlas,
        Material currentMaterial,
        Material newMaterial,
        int textureId)
    {
        var textureName = currentMaterial.GetTexture(textureId)?.name;

        if (textureName is null)
        {
            Debug.LogWarning($"атлас {atlas.name}, материа {currentMaterial.name} базовая текстура null");
            return;
        }

        var sprite = SpritesCache
            .GetValue(atlas.GetInstanceID())
            .FirstOrDefault(i => i.name == textureName);

        newMaterial.SetTexture(textureId, sprite!.texture);
        RefreshOffsetAndScale(sprite.uv, newMaterial, textureId);
    }

    private Vector2 RefreshOffsetAndScale(Vector2[] uv, Material material, int textureId)
    {
        var bottomLeft = new Vector2(float.MaxValue, float.MaxValue);
        var topRight = new Vector2(float.MinValue, float.MinValue);

        foreach (var v in uv)
        {
            bottomLeft.x = Math.Min(v.x, bottomLeft.x);
            bottomLeft.y = Math.Min(v.y, bottomLeft.y);

            topRight.x = Math.Max(v.x, topRight.x);
            topRight.y = Math.Max(v.y, topRight.y);
        }

        material.SetTextureOffset(textureId, bottomLeft);
        material.SetTextureScale(textureId, topRight - bottomLeft);
        return bottomLeft;
    }
}
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.Assertions;
using Voody.UniLeo.Lite;

public class UseComponentsWhileActive : MonoConstruct
{
    [SerializeField] private ConvertToEntity _entityRef;

    private BaseMonoProvider[] _monoProvider;
    private Context _context;
    private EcsWorld _world;
    
    private void OnValidate() =>
        _entityRef = _entityRef ? _entityRef : GetComponentInParent<ConvertToEntity>();

    protected override void Construct(Context context) => _context = context;

    private void Awake()
    {
        Assert.IsNotNull(_entityRef);
        _world = _context.Resolve<EcsWorld>();
        _monoProvider = GetComponents<BaseMonoProvider>();
    }

    private void OnEnable() => _entityRef.RegisterCallWhenEntityReady(Add);

    private void OnDisable()
    {
        if (_entityRef.RawEntity != -1)
            for (int i = 0, iMax = _monoProvider.Length; i < iMax; i++)
                _monoProvider[i].Remove(_entityRef.RawEntity, _world);

        _entityRef.UnRegisterCallWhenEntityReady(Add);
    }
    
    private void Add()
    {
        for (int i = 0, iMax = _monoProvider.Length; i < iMax; i++)
            _monoProvider[i].Attach(_entityRef.RawEntity, _world, true);
    }
}
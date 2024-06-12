using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.Assertions;
using Voody.UniLeo.Lite;

public class OnTriggerExitAddComponent : MonoConstruct, ITriggerDispatcherTarget
{
    [SerializeField] private BaseMonoProvider _monoProvider;
    [SerializeField] private ConvertToEntity _entityRef;
    [SerializeField] private bool _removeWithEnter;

    private Context _context;
    private EcsWorld _world;
    private int _contactCount;

    private void OnValidate() => _entityRef = _entityRef ? _entityRef : GetComponentInParent<ConvertToEntity>();

    protected override void Construct(Context context) => _context = context;

    private void Awake()
    {
        Assert.IsNotNull(_entityRef);
        _world = _context.Resolve<EcsWorld>();
    }
    
    private void Start()
    {
        if (_contactCount == 0)
            return;

        _monoProvider.Remove(_entityRef.RawEntity, _world);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (++_contactCount == 1 && _removeWithEnter && _entityRef.RawEntity != -1)
            _monoProvider.Remove(_entityRef.RawEntity, _world);
    }

    public void OnTriggerExit(Collider col)
    {
        if (--_contactCount == 0 && _entityRef.RawEntity != -1)
            _monoProvider.Attach(_entityRef.RawEntity, _world, true);
    }
}
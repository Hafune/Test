using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.Assertions;
using Voody.UniLeo.Lite;

public class OnTriggerEnter2DAddComponent : MonoConstruct, ITriggerDispatcherTarget2D
{
    [SerializeField] private BaseMonoProvider _monoProvider;
    [SerializeField] private ConvertToEntity _entityRef;
    [SerializeField] private bool _removeWithExit;

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

        _monoProvider.Attach(_entityRef.RawEntity, _world, true);
    }

    public void OnTriggerEnter2D(Collider2D _)
    {
        if (++_contactCount == 1 && _entityRef.RawEntity != -1)
            _monoProvider.Attach(_entityRef.RawEntity, _world, true);
    }

    public void OnTriggerExit2D(Collider2D _)
    {
        if (--_contactCount == 0 && _removeWithExit && _entityRef.RawEntity != -1)
            _monoProvider.Remove(_entityRef.RawEntity, _world);
    }
}
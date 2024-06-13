using Core;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;

public class ReceiverClient : MonoConstruct
{
    private const float _destinationTime = .03f;
    private MyList<Rigidbody> _items = new();
    private Vector3 _itemOffset = new(0, .5f, 0);
    private CollectorService _collectorService;
    private Context _context;

    protected override void Construct(Context context) => _context = context;

    private void Awake() => _collectorService = _context.Resolve<CollectorService>();

    private void OnEnable() => _items.Clear();

    public void AddItem(Rigidbody item) => _items.Add(item);

    private void FixedUpdate()
    {
        for (int i = 0, iMax = _items.Count; i < iMax; i++)
        {
            var item = _items[i];
            var position = i == 0 ? transform.position : _items[i - 1].position;
            var distance = position + _itemOffset - item.position;
            item.velocity = distance / _destinationTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _items.Clear();
        _collectorService.EnableReceiver(other.GetComponentInParent<ReceiverInstance>());
    }
}
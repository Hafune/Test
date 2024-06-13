using Core;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;

public class ReceiverClient : MonoConstruct
{
    private const float _destinationTime = .03f;
    private const float _receiverDestinationTime = .3f;
    private MyList<Rigidbody> _items = new();
    private Vector3 _itemOffset = new(0, .5f, 0);
    private ReceiverService _receiverService;
    private Context _context;

    protected override void Construct(Context context) => _context = context;

    private void Awake() => _receiverService = _context.Resolve<ReceiverService>();

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
        foreach (var item in _items)
            item.velocity = (other.transform.position - item.position) / _receiverDestinationTime;
        
        _items.Clear();
        _receiverService.EnableReceiver(other.GetComponentInParent<ReceiverInstance>());
    }
}
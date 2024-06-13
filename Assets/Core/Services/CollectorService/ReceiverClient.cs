using Core.Lib;
using UnityEngine;

public class ReceiverClient : MonoBehaviour
{
    private const float _destinationTime = .03f;
    private const float _receiverDestinationTime = .3f;
    private MyList<Rigidbody> _items = new();
    private Vector3 _itemOffset = new(0, .5f, 0);

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
    }
}
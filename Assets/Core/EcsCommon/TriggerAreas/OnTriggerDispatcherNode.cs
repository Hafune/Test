using UnityEngine;

public class OnTriggerDispatcherNode : MonoBehaviour, ITriggerDispatcherTarget2D
{
    private ITriggerDispatcherTarget2D[] _listeners;
    private int _contactCount;

    private void Awake()
    {
        _listeners = transform.parent.GetComponents<ITriggerDispatcherTarget2D>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (++_contactCount != 1)
            return;

        for (int i = 0; i < _listeners.Length; i++)
            _listeners[i].OnTriggerEnter2D(col);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (--_contactCount != 0)
            return;

        for (int i = 0; i < _listeners.Length; i++)
            _listeners[i].OnTriggerExit2D(col);
    }
}
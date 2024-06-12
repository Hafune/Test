using UnityEngine;

public interface ITriggerDispatcherTarget2D
{
    public void OnTriggerEnter2D(Collider2D col);
    public void OnTriggerExit2D(Collider2D col);
}
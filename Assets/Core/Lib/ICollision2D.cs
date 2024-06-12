using UnityEngine;

public interface ICollision2D
{
    public void OnCollisionEnter2D(Collision2D col);
    public void OnCollisionExit2D(Collision2D col);
}
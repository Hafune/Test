using UnityEngine;

public abstract class AbstractCompare : MonoBehaviour
{
    public abstract bool Compare(GameObject go);

    private void OnEnable()
    {
        
    }
}
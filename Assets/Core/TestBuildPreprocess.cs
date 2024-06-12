using UnityEngine;
using VInspector;

public class TestBuildPreprocess : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject target2;
    
    [Button]
    public void Test()
    {
        target2.transform.parent = target.transform;
    }
}
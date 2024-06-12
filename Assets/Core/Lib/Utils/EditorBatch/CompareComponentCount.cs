using UnityEngine;

public class CompareComponentCount : AbstractCompare
{
    public int count;
    public override bool Compare(GameObject go) => go.GetComponents<Component>().Length == count;
}
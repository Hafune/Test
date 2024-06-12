using UnityEngine;

public class CompareChildCount : AbstractCompare
{
    public int count;
    public override bool Compare(GameObject go) => go.transform.childCount == count;
}
using UnityEngine;

public class CompareChildCountNonZero : AbstractCompare
{
    public override bool Compare(GameObject go) => go.transform.childCount > 0;
}
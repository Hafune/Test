using UnityEngine;

public class CompareInvalidLayer : AbstractCompare
{
    public override bool Compare(GameObject go) => LayerMask.LayerToName(go.layer) == "";
}
using UnityEngine;

public class CompareParentNameStartsWith : AbstractCompare
{
    public string startsWith;
    public bool ignoreCase;

    public override bool Compare(GameObject go)
    {
        go = go.transform.parent?.gameObject;

        if (!go)
            return false;
        
        return ignoreCase
            ? go.name.ToLower().StartsWith(startsWith.ToLower())
            : go.name.StartsWith(startsWith);
    }
}
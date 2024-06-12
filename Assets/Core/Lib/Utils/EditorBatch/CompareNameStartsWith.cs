using UnityEngine;

public class CompareNameStartsWith : AbstractCompare
{
    public string startsWith;
    public bool ignoreCase;
    public bool notEqual;

    public override bool Compare(GameObject go)
    {
        var result = ignoreCase
            ? go.name.ToLower().StartsWith(startsWith.ToLower())
            : go.name.StartsWith(startsWith);

        return notEqual ? !result : result;
    }
}
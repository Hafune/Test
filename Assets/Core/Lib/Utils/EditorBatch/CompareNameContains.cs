using UnityEngine;

public class CompareNameContains : AbstractCompare
{
    public string startsWith;
    public bool ignoreCase;
    public bool notEqual;

    public override bool Compare(GameObject go)
    {
        var result = ignoreCase
            ? go.name.ToLower().Contains(startsWith.ToLower())
            : go.name.Contains(startsWith);

        return notEqual ? !result : result;
    }
}
using UnityEngine;

public class CompareHasMesh : AbstractCompare
{
    public override bool Compare(GameObject go)
    {
        var mf = go.GetComponent<MeshFilter>();

        return !mf ? false : mf.sharedMesh;
    }
}
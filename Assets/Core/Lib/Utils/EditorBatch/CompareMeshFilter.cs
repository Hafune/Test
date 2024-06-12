using System;
using UnityEngine;

public class CompareMeshFilter : AbstractCompare
{
    public Mesh mesh;

    public override bool Compare(GameObject go)
    {
        try
        {
            return mesh == go.GetComponent<MeshFilter>().sharedMesh;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
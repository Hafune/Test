using System;
using UnityEngine;

public class CompareMeshRenderer : AbstractCompare
{
    public Material material;

    public override bool Compare(GameObject go)
    {
        try
        {
            return material == go.GetComponent<MeshRenderer>().sharedMaterial;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
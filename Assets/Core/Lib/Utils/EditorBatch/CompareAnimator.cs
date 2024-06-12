using System;
using UnityEngine;

public class CompareAnimator : AbstractCompare
{
    public RuntimeAnimatorController controller;

    public override bool Compare(GameObject go)
    {
        try
        {
            return controller == go.GetComponent<Animator>().runtimeAnimatorController;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
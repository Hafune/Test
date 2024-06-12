using System;

public class EcsSystemAttribute : MyDoc
{
    public EcsSystemAttribute(string description, Type type = null) : base(description, type)
    {
    }
}
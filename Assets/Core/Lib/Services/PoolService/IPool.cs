using System;

public interface IPool : IDisposable
{
    public void ForceReturnInPool();
}
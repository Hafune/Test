using Leopotam.EcsLite;

namespace Core.Lib
{
    public interface IEcsEngine
    {
        public void AddUiSystem(IEcsSystem system);
    }
}
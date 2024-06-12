using Core.Systems;
using UnityEngine;

public class WaitForSecondsLogic : AbstractEntityResettableLogic
{
    [SerializeField] [Min(0)] private float _time;
    [SerializeField] private AbstractEntityLogic _next;
    private float _startTime;

    public override void ResetLogic(int entity) => _startTime = Time.time;

    public override void Run(int entity)
    {
        if (Time.time - _startTime > _time)
            _next.Run(entity);
    }
}
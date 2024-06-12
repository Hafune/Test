using Core.Components;
using Core.Lib;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class EventTimeDilationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventTimeDilation>> _filter;

        private readonly EcsPoolInject<EventTimeDilation> _pool;

        private readonly TimeScaleService _timeScaleService;
        private AnimationCurve _timeCurve;
        private float _time;
        private float _totalTime;

        public EventTimeDilationSystem(Context context) => _timeScaleService = context.Resolve<TimeScaleService>();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var timeCurve = _pool.Value.Get(i).timeCurve;
                var totalTime = timeCurve[timeCurve.length - 1].time;
                _pool.Value.Del(i);
                
                //Более длинное замедление в приоритете 
                if (totalTime < _totalTime - _time && timeCurve.length != 2)
                    continue;

                _timeCurve = timeCurve;
                _time = 0;
                _totalTime = _timeCurve[_timeCurve.length - 1].time;                
            }
            
            if (_time >= _totalTime)
                return;

            _timeScaleService.SetTimeDilation((_time += Time.unscaledDeltaTime) < _totalTime
                ? _timeCurve.Evaluate(_time)
                : 1);

            if (_timeScaleService.TimeDilation == 1)
                _totalTime = 0;
        }
    }
}
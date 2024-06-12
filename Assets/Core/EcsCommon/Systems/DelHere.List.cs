using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class DelHere<T> : IEcsRunSystem where T : struct
    {
        private readonly EcsFilterInject<Inc<T>> _filter;
        private readonly EcsPoolInject<T> _pool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pool.Value.Del(i);
        }
    }

    public class DelHere<T0, T1> : IEcsRunSystem
        where T0 : struct
        where T1 : struct
    {
        private readonly EcsFilterInject<Inc<T0>> _filter0;
        private readonly EcsPoolInject<T0> _pool0;

        private readonly EcsFilterInject<Inc<T1>> _filter1;
        private readonly EcsPoolInject<T1> _pool1;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter0.Value)
                _pool0.Value.Del(i);
            foreach (var i in _filter1.Value)
                _pool1.Value.Del(i);
        }
    }

    public class DelHere<T0, T1, T2> : IEcsRunSystem
        where T0 : struct
        where T1 : struct
        where T2 : struct
    {
        private readonly EcsFilterInject<Inc<T0>> _filter0;
        private readonly EcsPoolInject<T0> _pool0;

        private readonly EcsFilterInject<Inc<T1>> _filter1;
        private readonly EcsPoolInject<T1> _pool1;

        private readonly EcsFilterInject<Inc<T2>> _filter2;
        private readonly EcsPoolInject<T2> _pool2;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter0.Value)
                _pool0.Value.Del(i);
            foreach (var i in _filter1.Value)
                _pool1.Value.Del(i);
            foreach (var i in _filter2.Value)
                _pool2.Value.Del(i);
        }
    }

    public class DelHere<T0, T1, T2, T3> : IEcsRunSystem
        where T0 : struct
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        private readonly EcsFilterInject<Inc<T0>> _filter0;
        private readonly EcsPoolInject<T0> _pool0;

        private readonly EcsFilterInject<Inc<T1>> _filter1;
        private readonly EcsPoolInject<T1> _pool1;

        private readonly EcsFilterInject<Inc<T2>> _filter2;
        private readonly EcsPoolInject<T2> _pool2;

        private readonly EcsFilterInject<Inc<T3>> _filter3;
        private readonly EcsPoolInject<T3> _pool3;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter0.Value)
                _pool0.Value.Del(i);
            foreach (var i in _filter1.Value)
                _pool1.Value.Del(i);
            foreach (var i in _filter2.Value)
                _pool2.Value.Del(i);
            foreach (var i in _filter3.Value)
                _pool3.Value.Del(i);
        }
    }
    
    public class DelHere<T0, T1, T2, T3, T4> : IEcsRunSystem
        where T0 : struct
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        private readonly EcsFilterInject<Inc<T0>> _filter0;
        private readonly EcsPoolInject<T0> _pool0;

        private readonly EcsFilterInject<Inc<T1>> _filter1;
        private readonly EcsPoolInject<T1> _pool1;

        private readonly EcsFilterInject<Inc<T2>> _filter2;
        private readonly EcsPoolInject<T2> _pool2;

        private readonly EcsFilterInject<Inc<T3>> _filter3;
        private readonly EcsPoolInject<T3> _pool3;

        private readonly EcsFilterInject<Inc<T4>> _filter4;
        private readonly EcsPoolInject<T4> _pool4;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter0.Value)
                _pool0.Value.Del(i);
            foreach (var i in _filter1.Value)
                _pool1.Value.Del(i);
            foreach (var i in _filter2.Value)
                _pool2.Value.Del(i);
            foreach (var i in _filter3.Value)
                _pool3.Value.Del(i);
            foreach (var i in _filter4.Value)
                _pool4.Value.Del(i);
        }
    }
    
    public class DelHere<T0, T1, T2, T3, T4, T5> : IEcsRunSystem
        where T0 : struct
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        private readonly EcsFilterInject<Inc<T0>> _filter0;
        private readonly EcsPoolInject<T0> _pool0;

        private readonly EcsFilterInject<Inc<T1>> _filter1;
        private readonly EcsPoolInject<T1> _pool1;

        private readonly EcsFilterInject<Inc<T2>> _filter2;
        private readonly EcsPoolInject<T2> _pool2;

        private readonly EcsFilterInject<Inc<T3>> _filter3;
        private readonly EcsPoolInject<T3> _pool3;

        private readonly EcsFilterInject<Inc<T4>> _filter4;
        private readonly EcsPoolInject<T4> _pool4;

        private readonly EcsFilterInject<Inc<T5>> _filter5;
        private readonly EcsPoolInject<T5> _pool5;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter0.Value)
                _pool0.Value.Del(i);
            foreach (var i in _filter1.Value)
                _pool1.Value.Del(i);
            foreach (var i in _filter2.Value)
                _pool2.Value.Del(i);
            foreach (var i in _filter3.Value)
                _pool3.Value.Del(i);
            foreach (var i in _filter4.Value)
                _pool4.Value.Del(i);
            foreach (var i in _filter5.Value)
                _pool5.Value.Del(i);
        }
    }
    
    public class DelHere<T0, T1, T2, T3, T4, T5, T6> : IEcsRunSystem
        where T0 : struct
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        private readonly EcsFilterInject<Inc<T0>> _filter0;
        private readonly EcsPoolInject<T0> _pool0;

        private readonly EcsFilterInject<Inc<T1>> _filter1;
        private readonly EcsPoolInject<T1> _pool1;

        private readonly EcsFilterInject<Inc<T2>> _filter2;
        private readonly EcsPoolInject<T2> _pool2;

        private readonly EcsFilterInject<Inc<T3>> _filter3;
        private readonly EcsPoolInject<T3> _pool3;

        private readonly EcsFilterInject<Inc<T4>> _filter4;
        private readonly EcsPoolInject<T4> _pool4;

        private readonly EcsFilterInject<Inc<T5>> _filter5;
        private readonly EcsPoolInject<T5> _pool5;

        private readonly EcsFilterInject<Inc<T6>> _filter6;
        private readonly EcsPoolInject<T6> _pool6;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter0.Value)
                _pool0.Value.Del(i);
            foreach (var i in _filter1.Value)
                _pool1.Value.Del(i);
            foreach (var i in _filter2.Value)
                _pool2.Value.Del(i);
            foreach (var i in _filter3.Value)
                _pool3.Value.Del(i);
            foreach (var i in _filter4.Value)
                _pool4.Value.Del(i);
            foreach (var i in _filter5.Value)
                _pool5.Value.Del(i);
            foreach (var i in _filter6.Value)
                _pool6.Value.Del(i);
        }
    }
    
    public class DelHere<T0, T1, T2, T3, T4, T5, T6, T7> : IEcsRunSystem
        where T0 : struct
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
    {
        private readonly EcsFilterInject<Inc<T0>> _filter0;
        private readonly EcsPoolInject<T0> _pool0;

        private readonly EcsFilterInject<Inc<T1>> _filter1;
        private readonly EcsPoolInject<T1> _pool1;

        private readonly EcsFilterInject<Inc<T2>> _filter2;
        private readonly EcsPoolInject<T2> _pool2;

        private readonly EcsFilterInject<Inc<T3>> _filter3;
        private readonly EcsPoolInject<T3> _pool3;

        private readonly EcsFilterInject<Inc<T4>> _filter4;
        private readonly EcsPoolInject<T4> _pool4;

        private readonly EcsFilterInject<Inc<T5>> _filter5;
        private readonly EcsPoolInject<T5> _pool5;

        private readonly EcsFilterInject<Inc<T6>> _filter6;
        private readonly EcsPoolInject<T6> _pool6;

        private readonly EcsFilterInject<Inc<T7>> _filter7;
        private readonly EcsPoolInject<T7> _pool7;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter0.Value)
                _pool0.Value.Del(i);
            foreach (var i in _filter1.Value)
                _pool1.Value.Del(i);
            foreach (var i in _filter2.Value)
                _pool2.Value.Del(i);
            foreach (var i in _filter3.Value)
                _pool3.Value.Del(i);
            foreach (var i in _filter4.Value)
                _pool4.Value.Del(i);
            foreach (var i in _filter5.Value)
                _pool5.Value.Del(i);
            foreach (var i in _filter6.Value)
                _pool6.Value.Del(i);
            foreach (var i in _filter7.Value)
                _pool7.Value.Del(i);
        }
    }
}
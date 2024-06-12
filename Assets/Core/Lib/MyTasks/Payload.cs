using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Core
{
    public class Payload : IDisposable
    {
        private static readonly Stack<Payload> _pool = new();

        public static Payload GetPooled()
        {
            if (!_pool.TryPop(out var payload))
                return new Payload();

            payload._inPool = false;
            return payload;
        }

        private readonly Dictionary<Type, object> _payload;
        private bool _inPool;

        private Payload() => _payload = new();

        private Payload(Payload payload) => _payload = new(payload._payload);

        public T Get<T>()
        {
#if UNITY_EDITOR
            DebugCheck();
#endif
            return (T)_payload[typeof(T)];
        }

        public T GetOrInitialize<T>() where T : new()
        {
#if UNITY_EDITOR
            DebugCheck();
#endif
            if (_payload.TryGetValue(typeof(T), out var v))
                return (T)v;

            var value = new T();
            Set(value);
            return value;
        }

        public void Set<T>(T value)
        {
#if UNITY_EDITOR
            DebugCheck();
#endif
            _payload[typeof(T)] = value;
        }

#if UNITY_EDITOR
        private void DebugCheck()
        {
            if (_inPool)
                throw new Exception("TaskPayload используется после того как был положен в пул");
        }
#endif

        public Payload Copy()
        {
            var payload = GetPooled();
            payload._payload.AddRange(_payload);
            return payload;
        }

        public void Dispose()
        {
            if (_inPool)
                return;

            _payload.Clear();
            _pool.Push(this);
            _inPool = true;
        }
    }
}
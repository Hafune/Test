using System;
using UnityEngine;

namespace Core.Lib
{
    public class EnableDispatcher : MonoBehaviour
    {
        public int index;
        public event Action OnEnabled;
        public event Action OnDisabled;

        private void OnEnable() => OnEnabled?.Invoke();

        private void OnDisable() => OnDisabled?.Invoke();
    }
}
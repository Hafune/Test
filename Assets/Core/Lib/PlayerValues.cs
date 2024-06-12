using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public class PlayerValues
    {
        private PlayerPrefsWrapper instance;
        [ES3Serializable] private readonly Dictionary<string, int> _ints = new();
        [ES3Serializable] private readonly Dictionary<string, float> _floats = new();
        [ES3Serializable] private readonly Dictionary<string, string> _strings = new();
        private readonly Dictionary<string, EventActions> _actions = new();

        public int GetInt(string key, int defaultValue = default) =>
            _ints.ContainsKey(key) ? _ints[key] : _ints[key] = defaultValue;

        public float GetFloat(string key, float defaultValue = default) =>
            _floats.ContainsKey(key) ? _floats[key] : _floats[key] = defaultValue;

        public string GetString(string key, string defaultValue = default) =>
            _strings.ContainsKey(key) ? _strings[key] : _strings[key] = defaultValue;

        public void SetInt(string key, int value)
        {
            _ints[key] = value;
            CallEvents(key);
        }

        public void SetFloat(string key, float value)
        {
            _floats[key] = value;
            CallEvents(key);
        }

        public void SetString(string key, string value)
        {
            _strings[key] = value;
            CallEvents(key);
        }

        public int this[string key]
        {
            get => GetInt(key);
            set => SetInt(key, value);
        }

        public void Save()
        {
            _ints.ForEach(pair => PlayerPrefs.SetInt(pair.Key, pair.Value));
            _floats.ForEach(pair => PlayerPrefs.SetFloat(pair.Key, pair.Value));
            _strings.ForEach(pair => PlayerPrefs.SetString(pair.Key, pair.Value));
        }

        public void ResetUnsaved()
        {
            _ints.Clear();
            _floats.Clear();
            _strings.Clear();
        }

        public void AddEventListener(string variableName, Action action)
        {
            if (!_actions.ContainsKey(variableName))
                _actions[variableName] = new EventActions();

            _actions[variableName].callback += action;
        }

        public void RemoveEventListener(string variableName, Action action) =>
            _actions[variableName].callback -= action;

        private void CallEvents(string variableName)
        {
            if (!_actions.ContainsKey(variableName))
                return;
            
            _actions[variableName].callback?.Invoke();
        }

        private record EventActions
        {
            public Action callback;
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public class PlayerPrefsWrapper
    {
        private static PlayerPrefsWrapper instance;
        private static readonly Dictionary<string, int> _ints = new();
        private static readonly Dictionary<string, float> _floats = new();
        private static readonly Dictionary<string, string> _strings = new();
        private static readonly Dictionary<string, EventActions> _actions = new();

        public static PlayerPrefsWrapper Inst => instance ??= new PlayerPrefsWrapper();

        public static int GetInt(string key, int defaultValue = default) =>
            _ints.ContainsKey(key) ? _ints[key] : _ints[key] = PlayerPrefs.GetInt(key, defaultValue);

        public static float GetFloat(string key, float defaultValue = default) =>
            _floats.ContainsKey(key) ? _floats[key] : _floats[key] = PlayerPrefs.GetFloat(key, defaultValue);

        public static string GetString(string key, string defaultValue = default) =>
            _strings.ContainsKey(key) ? _strings[key] : _strings[key] = PlayerPrefs.GetString(key, defaultValue);

        public static void SetInt(string key, int value)
        {
            _ints[key] = value;
            CallEvents(key);
        }

        public static void SetFloat(string key, float value)
        {
            _floats[key] = value;
            CallEvents(key);
        }

        public static void SetString(string key, string value)
        {
            _strings[key] = value;
            CallEvents(key);
        }

        public int this[string key]
        {
            get => GetInt(key);
            set => SetInt(key, value);
        }

        public static void Save()
        {
            _ints.ForEach(pair => PlayerPrefs.SetInt(pair.Key, pair.Value));
            _floats.ForEach(pair => PlayerPrefs.SetFloat(pair.Key, pair.Value));
            _strings.ForEach(pair => PlayerPrefs.SetString(pair.Key, pair.Value));
        }

        public static void ResetUnsaved()
        {
            _ints.Clear();
            _floats.Clear();
            _strings.Clear();
        }

        public static void AddEventListener(string variableName, Action action)
        {
            if (!_actions.ContainsKey(variableName))
                _actions[variableName] = new EventActions();

            _actions[variableName].callback += action;
        }

        public static void RemoveEventListener(string variableName, Action action) =>
            _actions[variableName].callback -= action;

        private static void CallEvents(string variableName)
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
using System;
using System.Text;
using BehaviorDesigner.Runtime.Tasks;
using Core.Services;
using JetBrains.Annotations;
using Lib;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions"), Serializable]
    public class ActionCooldown : Conditional
    {
        [SerializeField] protected float _startDelay;
        [SerializeField] protected float _cooldown;
        [SerializeField] private bool _ignoreServiceScale;
        private float _lastUseTime;

#if UNITY_EDITOR
        [CanBeNull] private string _drawName;
        
        public override string OnDrawNodeText()
        {
            if (Application.isPlaying)
            {
                var percent = Math.Clamp(_cooldown == 0 ? 1 : (Time.time - _lastUseTime) / _cooldown, 0, 1);

                var builder = new StringBuilder("[          ]");
                for (int i = 0; i < 10; i++)
                    builder[1 + i] = percent * 10 >= i ? '-' : '_';

                return builder + $" {FormatUiValuesUtility.ToPercentInt(percent)}%";
            }

            _drawName ??= GetType().Name.FormatAddCharBeforeCapitalLetters();
            string newName = _drawName + " " + FormatUiValuesUtility.FloatDim1(_cooldown);

            if (FriendlyName == newName)
                return string.Empty;

            FriendlyName = newName;
            return string.Empty;
        }
#endif

        public override void OnAwake()
        {
            _lastUseTime = Time.time - _cooldown + _startDelay;

            if (_ignoreServiceScale)
                return;

            var service = GetComponent<ConvertToEntity>().Context.Resolve<BTreeActionTimingsService>();
            _cooldown *= service.GetCooldownScale();
        }

        public override TaskStatus OnUpdate()
        {
            if (_lastUseTime + _cooldown > Time.time)
                return TaskStatus.Failure;

            return TaskStatus.Success;
        }

        public void Restart() => _lastUseTime = Time.time;
    }
}
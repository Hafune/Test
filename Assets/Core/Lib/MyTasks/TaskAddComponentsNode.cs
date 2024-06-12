using System;
using System.Linq;
using Leopotam.EcsLite;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.Lib
{
    public class TaskAddComponentsNode : MonoBehaviour, IMyTask
    {
        [SerializeField] private BaseComponentTemplate[] _componentSO;

        private IBaseProvider[] _baseProviders;
        private BaseMonoProvider[] _baseMonoProviders;

        public bool InProgress => false;

        private void Awake()
        {
            _baseProviders = _componentSO.Select(i => i.Build()).ToArray();
            _baseMonoProviders = GetComponents<BaseMonoProvider>();
        }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            var world = context.Resolve<EcsWorld>();
            var _convertToEntity = payload.Get<ConvertToEntity>();

            for (int i = 0, iMax = _baseProviders.Length; i < iMax; i++)
                _baseProviders[i].Attach(_convertToEntity.RawEntity, world, true);

            for (int i = 0, iMax = _baseMonoProviders.Length; i < iMax; i++)
                _baseMonoProviders[i].Attach(_convertToEntity.RawEntity, world, true);
            
            onComplete?.Invoke(this);
        }
    }
}